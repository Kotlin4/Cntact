using Cntact.Contracts;
using FluentValidation;

namespace Cntact.Validators
{
    public class DeleteContactRequestValidator : AbstractValidator<DeleteContactRequest>
    {
        public DeleteContactRequestValidator()
        {
            RuleFor(x => x)
                .Must(request =>
                    !string.IsNullOrWhiteSpace(request.Number) ||
                    !string.IsNullOrWhiteSpace(request.FirstName) ||
                    !string.IsNullOrWhiteSpace(request.Name) ||
                    !string.IsNullOrWhiteSpace(request.LastName))
                .WithMessage("Необходимо указать хотя бы одно поле для поиска.");

            RuleFor(x => x.Number)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(x => !string.IsNullOrWhiteSpace(x.Number))
                .WithMessage("Некорректный номер телефона.");

            RuleFor(x => x.FirstName)
                .MaximumLength(50).WithMessage("Имя не может быть длиннее 50 символов.")
                .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Отчество не может быть длиннее 50 символов.")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Фамилия не может быть длиннее 50 символов.")
                .When(x => !string.IsNullOrWhiteSpace(x.LastName));
        }
    }
}
