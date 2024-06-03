using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;


[Table("Shopping_Carts")]
public class ShoppingCarts
{
    [Column("FK_account")]
    [ForeignKey("Account")]
    public int ShoppingCartsAccount { get; set; }
    
    [Column("FK_product")]
    [ForeignKey("Product")]
    public int ShoppingCartsProduct { get; set; }
    
    [Column("amount")]
    public int ShoppingCartsAmount { get; set; }
    
    public int Account { get; set; }
    public int Product { get; set; }
}