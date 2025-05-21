using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.UserPanel.Ticket;
using RakSport.Domain.Models.Ticket;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;

namespace RakSport.Infrastructure.Repositories.UserPanel.Ticket
{
    public class TicketRepository : GenericRepository<TicketEntity>, ITicketRepository
    {
        private readonly DataBaseContext _context;

        public TicketRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TicketEntity>> GetTicketByUserIdAsync(Guid userId, TicketEntity.TicketStatus? statusFilter)
        {
            var query = _context.Tickets.Where(t => t.UserId == userId);

            if (statusFilter.HasValue)
            {
                query = query.Where(t => t.Status == statusFilter.Value);
            }

            return await query
                .OrderByDescending(t => t.CreateAt)
                .ToListAsync();
        }

        public async Task<TicketEntity>? GetTicketWithMessagesByIdAsync(Guid ticketId)
        {
            return await _context.Tickets
                .Include(t => t.TicketMessages)
                .FirstOrDefaultAsync(t => t.Id == ticketId);                
        }
    }
}
