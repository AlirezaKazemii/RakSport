using RakSport.Domain.Models.Payment;
using RakSport.Domain.Models.ReservationItem;
using RakSport.Domain.Models.Review;
using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.Reservation
{
    public class ReservationEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreateAt { get; private set; }
        public decimal TotalAmount { get; private set; }
        public ReservationStatus Status { get; private set; }
        public ReservationType Type { get;private set; }
        public bool IsReviewed { get; private set; }   // برای جلوگیری از ثبت چند نظر برای یک رزرو
        public DateTime? CanceledAt { get; private set; }

        // Navigation Properties
        public UserEntity User { get; private set; }
        public ICollection<ReservationItemEntity> ReservationItems { get; private set; } 
        public PaymentEntity? Payment { get; private set; }
        public ReviewEntity Review { get; set; }


        public ReservationEntity(Guid userId, decimal totalAmount, ReservationStatus status, ReservationType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Status = status;
            Type = type;
            CreateAt = DateTime.Now;
            ReservationItems = new List<ReservationItemEntity>();
        }

        public enum ReservationStatus
        {
            pending,     // در انتظار پرداخت
            Paid,        // پرداخت شده
            Cancelled,   // لغو شده
            Refunded     // بازگشت وجه
        }


        public enum ReservationType
        {
            SingleSession,   // برای فوتسال و استخر
            Monthly          // برای رزمی
        } 


        public void Reviewed()
        {
            IsReviewed = true;
        }


        public void setCancelled()
        {
            Status = ReservationStatus.Cancelled;
            CanceledAt = DateTime.Now;
        }

        public void AddItem(ReservationItemEntity item)
        {
            item.SetReservation(this);
            ReservationItems.Add(item);
        }

    }
}
