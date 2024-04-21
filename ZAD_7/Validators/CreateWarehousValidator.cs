using FluentValidation;
using ZAD_7.DTOs;

namespace ZAD_7.Validators;

public class CreateWarehousValidator :AbstractValidator<CreateWarehouseProduct>
{
    public CreateWarehousValidator()
    {
        RuleFor(e => e.IdProduct).NotNull();
        RuleFor(e => e.IdWarehouse).NotNull();
        RuleFor(e => e.Amount).NotNull();
        RuleFor(e => e.CreatedAt).NotNull();
    }
}