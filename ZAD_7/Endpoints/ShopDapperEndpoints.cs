using System.Data;
using System.Data.SqlClient;
using Dapper;
using FluentValidation;
using ZAD_7.DTOs;
using ZAD_7.Models;

namespace ZAD_7.Endpoints;


public static class ShopDapperEndpoints
{
    public static void RegisterAnimalEndpoints(this WebApplication app)
    {
        var products = app.MapGroup("");
        products.MapPost("/api/WarehousProduct/", AddWarehousProduct);
        products.MapPost("/api/WarehousProductProc/", AddWarehousProductProc);
    }

    private static async Task<IResult> AddWarehousProduct(IConfiguration configuration, CreateWarehouseProduct request,
        IValidator<CreateWarehouseProduct> validator)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            var productWithId = await connection.QueryFirstAsync<Product>(
                "SELECT * FROM Product WHERE IdProduct=@IdPr",
                new{IdPr = request.IdProduct},
                transaction: transaction
            );
            
            var warehousWithIdExists = await connection.ExecuteAsync(
                "SELECT 1 FROM Warehouse WHERE IdWarehouse=@IdWarehouse",
                new
                {IdWarehouse = request.IdWarehouse},
                transaction: transaction
            );
            if (productWithId.IdProduct == 0 || warehousWithIdExists == 0){return Results.NotFound();}
            Console.WriteLine("1 ok --------------------------------------------------------");
            
            var idOrder = await connection.ExecuteScalarAsync<int>(
                @"SELECT TOP 1 o.IdOrder FROM ""Order"" o   
                        LEFT JOIN Product_Warehouse pw ON o.IdOrder=pw.IdOrder  
                        WHERE o.IdProduct= @IdPr AND o.Amount= @Am AND pw.IdProductWarehouse IS NULL AND  
                        o.CreatedAt< @CrAt;  ",
                new
                {
                    IdPr = request.IdProduct,
                    Am = request.Amount,
                    CrAt = request.CreatedAt
                },
                transaction: transaction
            );
            if (idOrder == 0 ){return Results.NotFound();}
            Console.WriteLine("2 ok --------------------------------------------------------");
            
            var command4 = await connection.ExecuteAsync(
                @"UPDATE ""Order"" SET  
                        FulfilledAt=@CreatedAt  
                        WHERE IdOrder=@IdOrder;",
                new
                {
                    CreatedAt = request.CreatedAt,
                    IdOrder = idOrder
                },
                transaction: transaction
            );
            Console.WriteLine(idOrder + " ============================");
            Console.WriteLine(productWithId.Price + " ============================");
            var insertEndGetWarhProd = await connection.QueryFirstAsync<ProductWarehouse>(
                @"INSERT INTO Product_Warehouse(IdWarehouse,   
                    IdProduct, IdOrder, Amount, Price, CreatedAt)  
                    VALUES(@IdWaho, @IdPr, @IdOrder, @Am, @Am*@Pr, @CrAt);",
                new
                {
                    IdWaho = request.IdWarehouse,
                    IdPr = request.IdProduct,
                    IdOrder = idOrder,
                    Am = request.Amount,
                    Pr = productWithId.Price,
                    CrAt = request.CreatedAt
                },
                transaction: transaction
            );
            
            await transaction.CommitAsync();
            return Results.Created($"product/{insertEndGetWarhProd.IdWarehouse}", new DetailsWarehouseProduct(insertEndGetWarhProd));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static async Task<IResult> AddWarehousProductProc(IConfiguration configuration, CreateWarehouseProduct request,
        IValidator<CreateWarehouseProduct> validator)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

        await using var connection = new SqlConnection(configuration.GetConnectionString("Default"));
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            /*var addProd = await connection.QueryFirstAsync<ProductWarehouse>(
                @"EXEC AddProductToWarehouse @IdPr, @IdWr, @Am, @CrAt;",
                new
                {
                    IdPr = request.IdProduct,
                    IdWr = request.IdWarehouse,
                    Am = request.Amount,
                    CrAt = request.CreatedAt
                },
                transaction: transaction
            );*/
            var addProd = await connection.QueryFirstAsync<ProductWarehouse>(
                @"AddProductToWarehouse",
                new
                {
                    IdProduct = request.IdProduct,
                    IdWarehouse = request.IdWarehouse,
                    Amount = request.Amount,
                    CreatedAt = request.CreatedAt
                },
                commandType: CommandType.StoredProcedure,
                transaction: transaction
            );
            
            return Results.Created($"product/{addProd.IdWarehouse}", new DetailsWarehouseProduct(addProd));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}