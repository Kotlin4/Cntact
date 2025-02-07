using Cntact.Contracts;
using FluentValidation;

namespace Cntact.Validators
{
    public class GetContactsRequestValidator : AbstractValidator<GetContactsRequest>
    {
        public GetContactsRequestValidator()
        {
            RuleFor(x => x.Search)
                .MaximumLength(50).WithMessage("Максимальная длина запроса - 50 символов.");

            RuleFor(x => x.SortOrder)
                .Must(value => value == null || value.ToLower() == "asc" || value.ToLower() == "desc")
                .WithMessage("SortOrder должен быть 'asc' или 'desc'.");
        }
    }
}
