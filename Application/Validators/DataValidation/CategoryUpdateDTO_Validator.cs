using Domain.DTOs;
using FluentValidation;

namespace Application.Validators
{
    public class CategoryUpdateDTO_Validator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTO_Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.DelayPrice)
                .GreaterThanOrEqualTo(0).WithMessage("DelayPrice must be non-negative.");
        }
    }
}