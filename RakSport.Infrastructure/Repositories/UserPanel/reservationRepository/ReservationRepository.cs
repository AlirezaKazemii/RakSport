using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.User.Rservation;
using RakSport.Domain.Models.Reservation;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Repositories.Genericrepository;


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
            .Include(r => r.ReservationItems).ThenInclude(t => t.TimeSlot).ThenInclude(s => s.SportHall).ThenInclude(r => r.reviews).Include(p => p.Payment).
            FirstOrDefaultAsync(r => r.Id == reservationId && r.UserId == userId);
        }
    }
}
