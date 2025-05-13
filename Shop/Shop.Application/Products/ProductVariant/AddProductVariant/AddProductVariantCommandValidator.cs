using Common.Application.Validation;
using FluentValidation;
using Shop.Application.Products.ProductVariant.AddProductVariant;

public class AddProductVariantCommandValidator : AbstractValidator<AddProductVariantCommand>
{
    public AddProductVariantCommandValidator()
    {

        RuleFor(r => r.SKU)
            .NotEmpty().WithMessage(ValidationMessages.required("SKU"))
            .Length(3, 20).WithMessage("SKU must be between 3 and 20 characters.")
            .Matches("^[a-zA-Z0-9]*$").WithMessage("SKU must contain only letters and numbers.");

        RuleFor(r => r.Price)
            .NotNull().WithMessage(ValidationMessages.required("قیمت باید مقدار داشته باشد"))
              .InclusiveBetween(5000, 50000000).WithMessage("قیمت باید بیشتر از پنج هزار تومن و کمتر از پنجاه ملیون باشد");

        RuleFor(r => r.Size)
             .MaximumLength(30).WithMessage("سایز نباید بیشتر از 30 کاراکتر داشته باشد");

        RuleFor(r => r.Color)
            .MaximumLength(30).WithMessage("رنگ نباید بیشتر از 30 کاراکتر داشته باشد");

        RuleFor(r => r.StockQuantity)
            .NotNull().WithMessage(ValidationMessages.required("موجودی نمیتواند خالی باشد"))
            .InclusiveBetween(0, 1000).WithMessage("موجودی باید بین صفر و یک هزار باشد");
        
        RuleFor(r => r.DiscountPercentage)
            .InclusiveBetween(0, 100).WithMessage("درصد باید بین عدد صفر و صد باشد");


    }

}