using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.ReservationItem;

namespace RakSport.Domain.Interfaces.UserPanel.ReservationItem
{
    public interface IReservationItemRepository : IGenericRepository<ReservationItemEntity>
    {
        Task<bool> IsTimeSlotAvailable(Guid timeSlotId, DateTime date);
    }
}
