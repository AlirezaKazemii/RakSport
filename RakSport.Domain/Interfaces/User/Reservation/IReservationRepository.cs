using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.Reservation;

namespace RakSport.Domain.Interfaces.User.Reservation
{
    public interface IReservationRepository : IGenericRepository<ReservationEntity>
    {

        Task<ReservationEntity?> GetReservationWithDetailsByIdAsync(Guid userId, Guid reservationId);
        Task<List<ReservationEntity?>> GetAllAsync(Guid userId, string? statusFilter); 
    }
}
