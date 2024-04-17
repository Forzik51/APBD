using System.Data.SqlClient;
using Dapper;
using FluentValidation;
using ZAD_.DTOs;
using ZAD_.Endpoints;
using ZAD_.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAnimalValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditAnimalValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();


app.RegisterAnimalEndpoints();
app.MapControllers();
app.Run();
