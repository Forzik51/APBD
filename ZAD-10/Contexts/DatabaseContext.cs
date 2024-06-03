using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Contexts;

// context.Employees.Where...

public class DatabaseContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ShoppingCarts> ShoppingCarts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected DatabaseContext()
    {
    }
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<ShoppingCarts>()
            .HasKey(sc => new { sc.ShoppingCartsAccount, sc.ShoppingCartsProduct });

        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductCategoryProduct, pc.ProductCategoryCategory });

        modelBuilder.Entity<Role>().HasData(
        
            new Role
            {
                RoleId = 1,
                RoleName = "marketing"
            }
        );
        
        
        
        modelBuilder.Entity<Product>().HasData(new List<Product>
        {
            new Product
            {
                ProductId = 1,
                ProductName = "prodname",
                ProductWeight = 10.5,
                ProductWidth = 80.10,
                ProductHeight = 30.3,
                ProductDepth = 20.2
            }
        });
        
        modelBuilder.Entity<ShoppingCarts>().HasData(new List<ShoppingCarts>
        {
            new ShoppingCarts
            {
                ShoppingCartsAccount = 1,
                ShoppingCartsProduct = 1,
                ShoppingCartsAmount = 5
            }
        }); 
        modelBuilder.Entity<Account>().HasData(new List<Account>
        {
            new Account
            {
                AccountId = 1,
                AccountRole = 1,
                AccountFirstName = "Agata",
                AccountLastName = "Mielanska",
                AccountEmail = "bvsdkub@fbh.drg",
                AccountPhone = null
            }
        });
        
        
        
        modelBuilder.Entity<Category>().HasData(new List<Category>
        {
            new Category
            {
                CategoryId = 1,
                CategoryName = "catname"
            }
        });
        
        modelBuilder.Entity<ProductCategory>().HasData(new List<ProductCategory>
        {
            new ProductCategory
            {
                ProductCategoryProduct = 1,
                ProductCategoryCategory = 1
            }
        });
        
        
    }
}