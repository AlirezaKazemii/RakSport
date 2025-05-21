using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.UserPanel.ReservationItem;
using RakSport.Domain.Models.ReservationItem;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;

namespace RakSport.Infrastructure.Repositories.UserPanel.reservationItemRepository
{
    public class ReservationItemRepository : GenericRepository<ReservationItemEntity>, IReservationItemRepository
    {
        private readonly DataBaseContext _context;

        public ReservationItemRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsTimeSlotAvailable(Guid timeSlotId, DateTime date)
        {
            return !await _context.ReservationItems.AnyAsync(ri => ri.TimeSlotId == timeSlotId && ri.Date == date);
        }
    }
}
