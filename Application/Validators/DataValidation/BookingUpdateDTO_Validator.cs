using Domain.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class BookingUpdateDTO_Validator : AbstractValidator<BookingUpdateDTO>
    {
        public BookingUpdateDTO_Validator()
        {
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate)
                                                  .WithMessage("StartDate must be earlier than EndDate");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
                                                 .WithMessage("EndDate must be later than StartDate");
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                                                  .WithMessage("StartDate must be today or in the future");
            RuleFor(x => x.ToolIds).NotEmpty()
                                                   .WithMessage("At least one tool must be selected for the booking.");

        }
    }
}
