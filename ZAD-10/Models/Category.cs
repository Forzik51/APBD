using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;

[Table("Categories")]
public class Category
{
    [Key]
    [Column("PK_category")]
    public int CategoryId { get; set; }
    
    [MaxLength(100)]
    [Column("name")]
    public string CategoryName { get; set; }
    
    public IEnumerable<ProductCategory> ProductCategories { get; set; }
}