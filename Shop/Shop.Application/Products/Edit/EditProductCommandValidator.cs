using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.BrandName)
        .NotEmpty().WithMessage(ValidationMessages.required("نام برند"));

            RuleFor(r => r.ColorHex)
          .NotEmpty().WithMessage(ValidationMessages.required("ColorHex"));

            RuleFor(r => r.Slug)
               .NotEmpty().WithMessage(ValidationMessages.required("Slug"));

            RuleFor(r => r.Description)
               .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

            RuleFor(r => r.ImageFile)
               .JustImageFile();
        }
    }
}
