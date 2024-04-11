using ZAD_5;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMockDb, MockDb>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/animals", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAllAnimals());
});

app.MapGet("/animals/{id}", (IMockDb mockDb, int id) =>
{
    var animal = mockDb.GetById(id);
    if (animal is null) return Results.NotFound();
    
    return Results.Ok(animal);
});

app.MapPost("/animals", (IMockDb mockDb, Animal animal) =>
{
    mockDb.AddAnimal(animal);
    return Results.Created();
});

app.MapPut("/animals/{id}", (IMockDb mockDb, int id, Animal animal) =>
{
    if (mockDb.GetById(id) is null) return Results.NotFound();
    
    mockDb.Edit(id, animal);
    return Results.Created();
});

app.MapDelete("/animals/{id}", (IMockDb mockDb, int id) =>
{
    if (mockDb.GetById(id) is null) return Results.NotFound();
    
    mockDb.Delete(id);
    return Results.Ok();
});

app.MapGet("/visits", (IMockDb mockDb) =>
{
    return Results.Ok(mockDb.GetAllVisits());
});

app.MapPost("/visits", (IMockDb mockDb, Visit visit) =>
{
    if (mockDb.GetById(visit.IdAnimal) is null) return Results.NotFound();
    mockDb.AddVisits(visit);
    return Results.Created();
});


app.Run();

