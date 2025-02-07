using Cntact.Contracts;
using FluentValidation;

namespace Cntact.Validators
{
    public class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequestValidator() 
        {
            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Номер телефона обязателен.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Некорректный номер телефона.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Фамилия объязательна")
                .MaximumLength(50).WithMessage("Максимальная длина фамилии - 50 символов.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя объязательно")
                .MaximumLength(50).WithMessage("Максимальная длина имени - 50 символов.");


        }
    }
}
