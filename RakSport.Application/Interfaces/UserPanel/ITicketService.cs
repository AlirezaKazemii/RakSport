using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicket;
using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicketDetail;
using RakSport.Application.DTOs.UserPanel.Ticket;
using System.Security.Claims;

namespace RakSport.Application.Interfaces.UserPanel
{
    public interface ITicketService
    {
        Task<List<GetUserTicketsDto>> GetUserTecketsAsync(GetUserTicketCommand command, ClaimsPrincipal User);
        Task<GetUserTicketDetailDto> GetUserTicketDetailAsync(GetUserTicketDetailCommand command, ClaimsPrincipal User);
    }
}
