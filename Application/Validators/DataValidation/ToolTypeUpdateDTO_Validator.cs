using Domain.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class ToolTypeUpdateDTO_Validator : AbstractValidator<ToolTypeUpdateDTO>
    {
        public ToolTypeUpdateDTO_Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool type name is required.");

            RuleFor(x => x.MinLoanDays)
                .GreaterThan(0).WithMessage("MinLoanDays must be greater than zero.");

            RuleFor(x => x.MaxLoanDays)
                .GreaterThanOrEqualTo(x => x.MinLoanDays)
                .WithMessage("MaxLoanDays must be greater than or equal to MinLoanDays.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
        }
    }
}