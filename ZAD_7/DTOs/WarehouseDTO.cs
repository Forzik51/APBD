using ZAD_7.Models;

namespace ZAD_7.DTOs;

public record CreateWarehouseProduct(int IdProduct, int IdWarehouse, int Amount, DateTime CreatedAt);

public record DetailsWarehouseProduct(
    int IdProductWarehouse,
    int IdWarehouse,
    int IdProduct,
    int IdOrder,
    int Amount,
    double Price,
    DateTime CreatedAt)
{
    public DetailsWarehouseProduct(ProductWarehouse prWr) : this(
        prWr.IdProductWarehouse,
        prWr.IdWarehouse,
        prWr.IdProduct,
        prWr.IdOrder,
        prWr.Amount,
        prWr.Price,
        prWr.CreatedAt){}

}