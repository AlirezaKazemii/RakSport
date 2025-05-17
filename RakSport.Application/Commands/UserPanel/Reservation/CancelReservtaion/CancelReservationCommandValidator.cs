using FluentValidation;

namespace RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion
{
    public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
    {
        public CancelReservationCommandValidator()
        {
            RuleFor(x => x.ReservationId).NotEmpty().WithMessage("Reservation Id is required.");

            RuleFor(x => x.ItemsIdsToCancel).NotEmpty().WithMessage("Items to cancel is required.");
        }
    }
}
