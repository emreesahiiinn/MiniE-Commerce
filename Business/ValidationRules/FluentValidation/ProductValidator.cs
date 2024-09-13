using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.ProductName).MinimumLength(2);
        RuleFor(x => x.ProductName).Must(MaxLengthSeven).WithMessage(Messages.Error);
    }

    private static bool MaxLengthSeven(string arg)
    {
        return arg.Length <= 7;
    }
}