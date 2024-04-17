using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ZAD_.DTOs;

public record GetAllAnimalsResponse(int IdAnimal, string Name, string Description, string Category, string Area);

public record EditAnimalResponse( string Name, string Description, string Category, string Area);


public record CreateAnimalResponse(string Name, string Description, string Category, string Area);


public record CreateAnimalRequest(
    [Required] [MaxLength(200)] string Name,
    [Required] [MaxLength(200)] string Description,
    [Required] [MaxLength(200)] string Category,
    [Required] [MaxLength(200)] string Area
    );