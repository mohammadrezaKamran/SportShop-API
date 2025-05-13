using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Comments.Edit
{
    public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
    {
        public EditCommentCommandValidator()
        {
            RuleFor(x => x.Text)
             .NotEmpty().WithMessage("کامنت نمی‌تواند خالی باشد")
             .MinimumLength(5).WithMessage("کامنت باید حداقل ۵ کاراکتر باشد")
             .MaximumLength(1000).WithMessage("کامنت نمی‌تواند بیشتر از ۱۰۰۰ کاراکتر باشد");
        }
    }
}