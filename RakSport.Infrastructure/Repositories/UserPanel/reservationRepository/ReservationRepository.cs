using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.User.Reservation;
using RakSport.Domain.Models.Reservation;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;
using static RakSport.Domain.Models.Reservation.ReservationEntity;


namespace RakSport.Infrastructure.Repositories.UserPanel.reservationRepository
{
    public class ReservationRepository : GenericRepository<ReservationEntity>, IReservationRepository
    {
        private readonly DataBaseContext _context;
        public ReservationRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ReservationEntity?> GetReservationWithDetailsByIdAsync(Guid userId, Guid reservationId)
        {
            return await _context.Reservations
            .Include(r => r.ReservationItems).ThenInclude(t => t.TimeSlot).ThenInclude(s => s.SportHall)
            .ThenInclude(i => i.Images)
            .Include(r => r.ReservationItems).ThenInclude(t => t.TimeSlot).ThenInclude(s => s.SportHall).ThenInclude(r => r.RefundPolicy)
            .Include(r => r.ReservationItems).ThenInclude(t => t.TimeSlot).ThenInclude(s => s.SportHall).ThenInclude(r => r.reviews).Include(p => p.Payment).
            FirstOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);
        }

        public async Task<List<ReservationEntity?>> GetAllAsync(Guid userId, string? statusFilter)
        {
            var query = _context.Reservations
                .Include(r => r.ReservationItems)
                    .ThenInclude(ri => ri.TimeSlot)
                        .ThenInclude(ts => ts.SportHall)
                            .ThenInclude(h => h.Images)
                .Include(r => r.ReservationItems)
                    .ThenInclude(ri => ri.TimeSlot)
                        .ThenInclude(ts => ts.SportHall)
                            .ThenInclude(h => h.reviews)
                .Include(r => r.Payment)
                .Where(r => r.UserId == userId)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                var today = DateTime.Now;

                statusFilter.ToLower();

                switch (statusFilter)
                {
                    case "Cancelled":
                        query = query.Where(r => r.Status == ReservationStatus.Cancelled);
                        break;
                    case "current":
                        query = query.Where(r =>
                        r.Status == ReservationStatus.Paid &&
                        r.ReservationItems.Max(ri => ri.TimeSlot.Date) > today);
                        break;
                    case "past":
                        query = query.Where(r =>
                        r.Status == ReservationStatus.Paid &&
                        r.ReservationItems.Max(ri => ri.TimeSlot.Date) <= today);
                        break;
                }
            }
            return await query.ToListAsync();
        }
    }
}
