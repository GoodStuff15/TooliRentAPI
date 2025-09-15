using Domain.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class ToolUpdateDTO_Validator : AbstractValidator<ToolUpdateDTO>
    {
        public ToolUpdateDTO_Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool name is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.ToolTypeId)
                .GreaterThan(0).WithMessage("ToolTypeId must be greater than zero.");
        }
    }
}