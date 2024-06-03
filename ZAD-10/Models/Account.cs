using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication7.Models;


[Table("Accounts")]
public class Account
{
    [Key]
    [Column("PK_account")]
    public int AccountId { get; set; }
    
    [Column("FK_role")]
    [ForeignKey("Role")]
    public int AccountRole { get; set; }
    
    [MaxLength(50)]
    [Column("first_name")]
    public string AccountFirstName { get; set; }
    
    [MaxLength(50)]
    [Column("last_name")]
    public string AccountLastName { get; set; }
    
    [MaxLength(80)]
    [Column("email")]
    public string AccountEmail { get; set; }
    
    [MaxLength(9)]
    [Column("phone")]
    public string? AccountPhone { get; set; }
    
    
    public int RoleId { get; set; }
    public IEnumerable<ShoppingCarts> ShoppingCarts { get; set; }
}