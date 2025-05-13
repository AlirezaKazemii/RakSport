using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Commands.UserPanel.User.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserInfoCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FullName)
           .MinimumLength(5).WithMessage("Full name must be at least 5 characters.")
           .MaximumLength(50).WithMessage("Full name must not exceed 50 characters.");

            RuleFor(x => x.Email)
           .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
           .WithMessage("Please enter a valid input");

            RuleFor(x => x.ShabaNumber)
           .Matches(@"^IR00\d{24}$").WithMessage("ShabaNumber format is not correct");
        }
    }
}