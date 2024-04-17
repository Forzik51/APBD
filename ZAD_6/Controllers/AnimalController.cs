using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using ZAD_.DTOs;

namespace ZAD_.Controllers;

[ApiController]
[Route("api/animalss")]
public class AnimalController : ControllerBase
{
    private readonly IConfiguration _configuration;
    
    public AnimalController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
    [HttpGet]
    public IActionResult GetAllAnimals(string orderBy)
    {
        orderBy.ToLower();
        if (orderBy == "" || 
            orderBy != "idanimal"|| orderBy != "name"|| orderBy != "description"|| orderBy != "category"|| orderBy != "area")
            orderBy = "Name";
        string command = "Select * from animal ORDER BY " + orderBy;
        var response = new List<GetAllAnimalsResponse>();
        using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
        {
            var sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.Connection.Open();
            var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                response.Add(new GetAllAnimalsResponse(
                        reader.GetInt32(0), 
                        reader.GetString(1), 
                        reader.GetString(2), 
                        reader.GetString(3), 
                        reader.GetString(4)
                    )
                );
            }
        }
        return Ok(response);
    }
}