using FluentValidation;

namespace RakSport.Application.Commands.UserPanel.Reservation.GetReservationSessions
{
    public class GetReservationSessionsCommandValidator : AbstractValidator<GetReservationSessionsCommand>
    {
        public GetReservationSessionsCommandValidator()
        {
            RuleFor(x => x.ReservationId).NotEmpty().WithMessage("Reservation Id is required.");
        }
    }
}
