using FluentValidation;

namespace RakSport.Application.Commands.UserPanel.Ticket.GetUserTicketDetail
{
    public class GetUserTicketDetailCommandValidator : AbstractValidator<GetUserTicketDetailCommand>
    {
        public GetUserTicketDetailCommandValidator()
        {
            RuleFor(x => x.TicketId).NotEmpty().WithMessage("Ticket id is required.");
        }
    }
}
