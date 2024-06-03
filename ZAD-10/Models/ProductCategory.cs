using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models;


[Table("Products_Categories")]
public class ProductCategory
{
    [Column("FK_product")]
    [ForeignKey("Product")]
    public int ProductCategoryProduct { get; set; }
    
    [Column("FK_category")]
    [ForeignKey("Category")]
    public int ProductCategoryCategory { get; set; }
    
    
    public int Category { get; set; }
    public int Product { get; set; }
}