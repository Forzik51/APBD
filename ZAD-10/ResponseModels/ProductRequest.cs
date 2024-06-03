namespace WebApplication7.ResponseModels;

public class ProductRequest
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public double ProductWeight { get; set; }
    public double ProductWidth { get; set; }
    public double ProductHeight { get; set; }
    public double ProductDepth { get; set; }
    public List<CatResp> ProductCategories { get; set; }
}