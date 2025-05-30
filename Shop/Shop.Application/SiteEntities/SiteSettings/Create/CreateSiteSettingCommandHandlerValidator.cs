using Common.Application.Validation;
using FluentValidation;
using Shop.Application.SiteEntities.SiteSettings.Create;

public class CreateSiteSettingCommandHandlerValidator : AbstractValidator<CreateSiteSettingCommand>
{
    public CreateSiteSettingCommandHandlerValidator()
    {
        RuleFor(r => r.Key)
        .NotNull().WithMessage(ValidationMessages.required("key"));

        RuleFor(r => r.Value)
                .NotNull()
                .NotEmpty().WithMessage(ValidationMessages.required("value"));

        RuleFor(x => x.Description)
          .NotEmpty().WithMessage("توضیحات نمی‌تواند خالی باشد")
          .MinimumLength(5).WithMessage("توضیحات باید حداقل ۵ کاراکتر باشد")
          .MaximumLength(500).WithMessage("توضیحات نمی‌تواند بیشتر از 500 کاراکتر باشد");
    }
}
