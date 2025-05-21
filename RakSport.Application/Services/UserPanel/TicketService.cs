using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicket;
using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicketDetail;
using RakSport.Application.DTOs.UserPanel.Ticket;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.UserPanel.Ticket;
using System.Security.Claims;

namespace RakSport.Application.Services.UserPanel
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<GetUserTicketsDto>> GetUserTecketsAsync(GetUserTicketCommand command, ClaimsPrincipal User)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (loggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUser);

            var statusFilter = command.ParsedStatus();

            var tickets = await _ticketRepository.GetTicketByUserIdAsync(userId, statusFilter);

            if (tickets is null)
                throw new Exception("Ticket not found");

            return tickets.Select(t => new GetUserTicketsDto
            {
                Subject = t.Subject,
                Status = t.Status.ToString(),
                Date = t.CreateAt
            }).ToList();
        }

        public async Task<GetUserTicketDetailDto> GetUserTicketDetailAsync(GetUserTicketDetailCommand command, ClaimsPrincipal User)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(loggedInUser is null)
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUser);

            var ticket = await _ticketRepository.GetTicketWithMessagesByIdAsync(command.TicketId);

            if (ticket is null)
                throw new Exception("Ticket not found");

            if(ticket.UserId !=  userId)
                throw new UnauthorizedAccessException();

            return new GetUserTicketDetailDto
            {
                Subject = ticket.Subject,
                Status = ticket.Status.ToString(),
                CreateAt = ticket.CreateAt,
                Messages = ticket.TicketMessages
                    .OrderBy(m => m.SentAt)
                    .Select(m => new TicketMessageDto
                    {
                        Sender = m.Sender.ToString(),
                        Message = m.Message,
                        SentAt = m.SentAt
                    }).ToList()
            };
        }
    }
}
