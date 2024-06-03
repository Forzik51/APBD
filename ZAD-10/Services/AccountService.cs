using Microsoft.EntityFrameworkCore;
using WebApplication7.Contexts;
using WebApplication7.Exceptions;
using WebApplication7.Models;
using WebApplication7.ResponseModels;

namespace WebApplication7.Services;


public interface IAccountService
{
    Task<GetAccountResponseModel> GetEmployeeByIdAsync(int id);
    Task<ProductRequest> ProductRequest(Product product, List<int> categoryIds);
}


public class AccountService(DatabaseContext context) :IAccountService
{
    public async Task<GetAccountResponseModel> GetEmployeeByIdAsync(int id)
    {
        var result1 = await context.Accounts
            .Where(x => x.AccountId == id)
            .Select(e => new GetAccountResponseModel
            {
                AccountFirstName = e.AccountFirstName,
                AccountLastName = e.AccountLastName,
                AccountEmail = e.AccountEmail,
                AccountPhone = e.AccountPhone,
            }).FirstOrDefaultAsync();
        
        var result2 = await context.Roles
            .Join(context.Accounts,role => role.RoleId,acc => acc.AccountRole,(role,acc) => 
                new
                {
                    role,acc
                })
            .Where(x => x.role.RoleId == x.acc.AccountRole)
            .Select(e => new
            {
                e.role.RoleName
            }).FirstOrDefaultAsync();
        
        var result3 = await context.Products
            .Join(context.ShoppingCarts,productId => productId.ProductId,shoppingcarts => shoppingcarts.ShoppingCartsProduct,(product,shoppingCarts) => 
                new
                {
                    product,shoppingCarts
                })
            .Where(x => x.shoppingCarts.ShoppingCartsAccount == id)
            .Select(e => new ProdResp
            {
                ProductId = e.product.ProductId,
                ProductName = e.product.ProductName,
                Amount = e.shoppingCarts.ShoppingCartsAmount
            }).ToListAsync();

        if (result1 is null)
        {
            throw new NotFoundException($"Account with id:{id} does not exist");
        }

        result1.AccountRole = result2.RoleName;
        result1.cart = result3;

        return result1;
    }

    public async Task<ProductRequest> ProductRequest(Product product, List<int> categoryIds)
    {
        context.Products.Add(product);
        context.SaveChangesAsync();
        
        var productCategories = categoryIds.Select(categoryId => new ProductCategory
        {
            ProductCategoryProduct = product.ProductId,
            ProductCategoryCategory = categoryId
        }).ToList();

        context.ProductCategories.AddRange(productCategories);
        await context.SaveChangesAsync();
        
        var result = await context.Products
            .Where(x => x.ProductId == product.ProductId)
            .Select(e => new ProductRequest
            {
                ProductId = e.ProductId,
                ProductName = e.ProductName,
                ProductWeight = e.ProductWeight,
                ProductWidth = e.ProductWidth,
                ProductHeight = e.ProductHeight,
                ProductDepth = e.ProductDepth,
            }).FirstOrDefaultAsync();
        
        var result2 = await context.ProductCategories
            .Where(x => x.ProductCategoryProduct == product.ProductId)
            .Join(context.Categories,prodcat => prodcat.ProductCategoryCategory,categ => categ.CategoryId,
                (prodcat,categ) => new{categ})
            .Select(e => new CatResp
            {
                CategoryId = e.categ.CategoryId,
                Name = e.categ.CategoryName
            }).ToListAsync();
        result.ProductCategories = result2.ToList();

        return result;

    }
}