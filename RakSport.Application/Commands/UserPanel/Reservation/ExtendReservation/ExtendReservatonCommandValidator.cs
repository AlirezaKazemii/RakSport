using FluentValidation;

namespace RakSport.Application.Commands.UserPanel.Reservation.ExtendReservation
{
    public class ExtendReservatonCommandValidator : AbstractValidator<ExtendReservatonCommand>
    {
        public ExtendReservatonCommandValidator()
        {
            RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("Reservation id is required.");

            RuleFor(x => x.MonthCount)
                .InclusiveBetween(1, 3).WithMessage("Minimum reservation is 1 month and maximum is 3 months.");
        }
    }
}
