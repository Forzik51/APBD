using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using FluentValidation;
using ZAD_.DTOs;
using ZAD_.Validators;

namespace ZAD_.Endpoints;

public static class AnimalsDapperEndpoints
{
    public static void RegisterAnimalEndpoints(this WebApplication app)
    {
        var animals = app.MapGroup("");

        animals.MapGet("api/animals", GetAnimals);
        animals.MapPost("/api/animals", CreateAnimal);
        animals.MapPut("/api/animals/{idAnimal:int}", EditAnimal);
        animals.MapDelete("/api/animals/{idAnimal:int}", DeleteAnimal);
    }

    public static IResult GetAnimals(IConfiguration configuration, string orderBy)
    {
        orderBy.ToLower();
        if (orderBy == "" || 
            orderBy != "idanimal"|| orderBy != "name"|| orderBy != "description"|| orderBy != "category"|| orderBy != "area")
            orderBy = "Name";
        string command = "Select * from animal ORDER BY " + orderBy;
        using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
        {
            var animals = sqlConnection.Query<GetAllAnimalsResponse>(
                command
            );
            return Results.Ok(animals);
        }
    }

    public static IResult CreateAnimal(IConfiguration configuration, CreateAnimalResponse request,
        IValidator<CreateAnimalResponse> validator)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

        using (var sqlConection = new SqlConnection(configuration.GetConnectionString("Default")))
        {
            var id = sqlConection.ExecuteScalar<int>(
                "INSERT INTO Animal (Name, Description, Category, Area) values (@Nm, @Ds, @Ct, @Ar)",
                new
                {
                    Nm = request.Name,
                    Ds = request.Description,
                    Ct = request.Category,
                    Ar = request.Area

                });
            return Results.Created();
        }
    }

    public static IResult EditAnimal(IConfiguration configuration, EditAnimalResponse request,
        IValidator<EditAnimalResponse> validator, int idAnimal)
    {
        var validation = validator.Validate(request);
        if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());

        using (var sqlConection = new SqlConnection(configuration.GetConnectionString("Default")))
        {
            var affectedRows =sqlConection.Execute(
                "UPDATE Animal SET Name = @Nm, Description = @Ds, Category = @Ct, Area = @Ar WHERE IdAnimal = @Id",
                new
                {
                    Nm = request.Name,
                    Ds = request.Description,
                    Ct = request.Category,
                    Ar = request.Area,
                    Id = idAnimal

                });
            if (affectedRows == 0) return Results.NotFound();
        }
        return Results.NoContent();
    }

    public static IResult DeleteAnimal(IConfiguration configuration, int idAnimal)
    {
        using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
        {

        var affectedRows = sqlConnection.Execute(
            "DELETE FROM Animal WHERE IdAnimal = @Id ",
            new {Id = idAnimal}
        );
        return affectedRows == 0 ? Results.NotFound() : Results.NoContent();
        }

    }

}
