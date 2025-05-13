using FluentValidation;

namespace RakSport.Application.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(3).WithMessage("The phone number must be at least 3 digits.")
                .MaximumLength(11).WithMessage("The phone number must be a maximum of 11 digits.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("New Password is required.");
        }
    }
}
