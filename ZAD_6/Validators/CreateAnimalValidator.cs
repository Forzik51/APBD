using FluentValidation;
using ZAD_.DTOs;

namespace ZAD_.Validators;

public class CreateAnimalValidator : AbstractValidator<CreateAnimalResponse>
{
    public CreateAnimalValidator()
    {
        RuleFor(e => e.Name).MaximumLength(200).NotNull();
        RuleFor(e => e.Description).MaximumLength(200);
        RuleFor(e => e.Category).MaximumLength(200).NotNull();
        RuleFor(e => e.Area).MaximumLength(200).NotNull();
    }
    
}