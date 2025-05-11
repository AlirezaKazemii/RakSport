using FluentValidation;

namespace RakSport.Application.Commands.Auth
{
    public class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
    {
        public SendOtpCommandValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(3).WithMessage("The phone number must be at least 3 digits.")
                .MaximumLength(11).WithMessage("The phone number must be a maximum of 11 digits.");
        }
    }
}
