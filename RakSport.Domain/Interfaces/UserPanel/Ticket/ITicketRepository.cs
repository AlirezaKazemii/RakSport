using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.Ticket;

namespace RakSport.Domain.Interfaces.UserPanel.Ticket
{
    public interface ITicketRepository : IGenericRepository<TicketEntity>
    {
        Task<List<TicketEntity>> GetTicketByUserIdAsync(Guid userId,TicketEntity.TicketStatus? statusFilter);
        Task<TicketEntity>? GetTicketWithMessagesByIdAsync(Guid ticketId);
    }
}
