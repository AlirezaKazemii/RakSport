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
        public bool IsCancelled { get; private set; }
        public DateTime Date { get; private set; }

        // Navigation Properties
        public TimeSlotEntity TimeSlot { get; private set; }
        public ReservationEntity Reservation { get; private set; }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public ReservationItemEntity(DateTime date, Guid timeSlotId, decimal price)
        {
            Id = Guid.NewGuid();
            TimeSlotId = timeSlotId;
            Price = price;
            Date = date;
        }

        public void SetReservation(ReservationEntity reservation)
        {
            Reservation = reservation;
        }

        public void SetTimeSlot(TimeSlotEntity timeSlot)
        {
            TimeSlot = timeSlot;
        }
    }
}
