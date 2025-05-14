using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.User.Reservation;
using RakSport.Domain.Models.Reservation;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;
using static RakSport.Domain.Models.Reservation.ReservationEntity;

namespace RakSport.Infrastructure.Repositories.UserPanel.rservationRepository
{
    public class ReservationRepository : GenericRepository<ReservationEntity>, IReservationRepository
    {
        private readonly DataBaseContext _context;

        public ReservationRepository(DataBaseContext context) : base(context)
        {
            _context = context;
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
