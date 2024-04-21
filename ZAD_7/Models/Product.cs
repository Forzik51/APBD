namespace ZAD_7.Models;

public class Product
{
    public int IdProduct { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
}