using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommandValidator : AbstractValidator<CreateSliderCommand>
{
    public CreateSliderCommandValidator()
    {
        RuleFor(r => r.ImageFile)
            .NotNull().WithMessage(ValidationMessages.required("عکس"))
            .JustImageFile();

		RuleFor(x => x.Link)
				.NotEmpty().WithMessage("لینک بنر را وارد کنید.")
				.MaximumLength(1000).WithMessage("لینک نمی‌تواند بیشتر از 1000 کاراکتر باشد.");

		RuleFor(x => x.Order)
			.GreaterThan(0).WithMessage("اولویت باید بزرگ‌تر از صفر باشد.")
			.LessThanOrEqualTo(30).WithMessage("اولویت نباید بیشتر از ۳۰ باشد.");

		RuleFor(x => x.AltText)
			.NotEmpty().WithMessage("متن جایگزین (AltText) الزامی است.")
			.MaximumLength(255).WithMessage("متن جایگزین نباید بیشتر از ۲۵۵ کاراکتر باشد.");

		RuleFor(x => x.Title)
			.MaximumLength(100).WithMessage("عنوان بنر نباید بیشتر از ۱۰۰ کاراکتر باشد.");

		RuleFor(x => x.Description)
			.MaximumLength(500).WithMessage("توضیحات بنر نباید بیشتر از ۵۰۰ کاراکتر باشد.");
	}
}