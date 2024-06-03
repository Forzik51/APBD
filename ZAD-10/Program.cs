using Microsoft.EntityFrameworkCore;
using WebApplication7.Contexts;
using WebApplication7.Exceptions;
using WebApplication7.Models;
using WebApplication7.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/accounts/{id:int}", async (int id, IAccountService service) =>
{
    try
    {
        return Results.Ok(await service.GetEmployeeByIdAsync(id));
    }
    catch (NotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
});

app.MapPost("api/products", async (Product product,List<int> categoryIds, IAccountService service) =>
{
    try
    {
        var result = await service.ProductRequest(product, categoryIds);
        return Results.Created($"/api/products/{result.ProductId}", result);
    }
    catch (NotFoundException e)
    {
        return Results.NotFound(e.Message);
    }
});


app.Run();
