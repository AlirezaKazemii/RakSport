using FluentValidation;

namespace RakSport.Application.Commands.Auth.ForgotPassword
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(3).WithMessage("The phone number must be at least 3 digits.")
                .MaximumLength(11).WithMessage("The phone number must be a maximum of 11 digits.");

            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("Otp Code is required.")
                .MinimumLength(6).WithMessage("Otp code must be 6 digits.")
                .MaximumLength(6).WithMessage("Otp code must be 6 digits.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required.");

            RuleFor(x => x.RepeatPassword)
                .NotEmpty().WithMessage("Repeat Password is required.");
        }
    }
}
