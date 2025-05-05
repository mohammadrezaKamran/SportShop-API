using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Products.ProductVariant.AddProductVariant;

public class AddProductVariantCommandValidator : AbstractValidator<AddProductVariant>
{
    public AddProductVariantCommandValidator()
    {

        RuleFor(r => r.SKU)
            .NotEmpty().WithMessage(ValidationMessages.required("SKU"))
            .Length(3, 20).WithMessage("SKU must be between 3 and 20 characters.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("SKU must contain only letters and numbers.");

        RuleFor(r => r.Price)
            .NotNull().WithMessage(ValidationMessages.required("Price"))
            .GreaterThanOrEqualTo(0).WithMessage("Price must be zero or greater.");

        RuleFor(r => r.Size)
            .NotEmpty().WithMessage(ValidationMessages.required("Size"));

        RuleFor(r => r.Color)
            .NotEmpty().WithMessage(ValidationMessages.required("Color"))
            .MaximumLength(30).WithMessage("Color name must not exceed 30 characters.");

        RuleFor(r => r.StockQuantity)
            .NotNull().WithMessage(ValidationMessages.required("Stock Quantity"))
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        RuleFor(r => r.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");


    }

}