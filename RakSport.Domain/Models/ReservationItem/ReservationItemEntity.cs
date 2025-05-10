using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.TimeSlot;

namespace RakSport.Domain.Models.ReservationItem
{
    public class ReservationItemEntity
    {
        public Guid Id { get; private set; }
        public Guid TimeSlotId { get; private set; }
        public Guid ReservationId { get; private set; }
        public decimal Price { get; private set; } // قیمت نهایی سانس در لحظه رزرو

        // Navigation Properties
        public TimeSlotEntity TimeSlot { get; private set; }
        public ReservationEntity Reservation { get; private set; }
    }
}
