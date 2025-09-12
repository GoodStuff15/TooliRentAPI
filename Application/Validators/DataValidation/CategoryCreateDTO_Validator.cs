using Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.DataValidation
{
    public class CategoryCreateDTO_Validator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTO_Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.DelayPrice)
                .NotEmpty().WithMessage("DelayPrice is required.");
        }
    }
}
