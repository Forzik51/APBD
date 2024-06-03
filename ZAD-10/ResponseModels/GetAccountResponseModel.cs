namespace WebApplication7.ResponseModels;

public class GetAccountResponseModel
{
    public string AccountFirstName { get; set; }
    public string AccountLastName { get; set; }
    public string AccountEmail { get; set; }
    public string? AccountPhone { get; set; }
    public string AccountRole { get; set; }
    
    public List<ProdResp> cart { get; set; }
}