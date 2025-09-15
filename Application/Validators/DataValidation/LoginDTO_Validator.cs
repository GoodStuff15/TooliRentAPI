using Domain.DTOs.IdentityDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.DataValidation
{
    public class LoginDTO_Validator : AbstractValidator<LoginDTO>
    {
        public LoginDTO_Validator() 
        {
            RuleFor(x =>  x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();
        
        }
    }
}
