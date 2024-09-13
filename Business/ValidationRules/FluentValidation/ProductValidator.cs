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
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(10).When(x => x.CategoryId == 1);

        // Adding Personal Rules and Customised Error Message
        RuleFor(x => x.ProductName).Must(MaxLengthSeven).WithMessage(Messages.Error);
    }

    private static bool MaxLengthSeven(string arg)
    {
        return arg.Length <= 7;
    }
}