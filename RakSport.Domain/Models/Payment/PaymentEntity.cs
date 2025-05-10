using RakSport.Domain.Models.Reservation;

namespace RakSport.Domain.Models.Payment
{
    public class PaymentEntity
    {
        public Guid Id { get; private set; }
        public Guid ReservationId { get;private set; }
        public decimal Amount { get; private set; }
        public string? GetWayTrackingCode { get; private set; }  // کد رهگیری از طرف درگاه پرداخت
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? GatewayName { get; private set; } // نام درگاه 
        public string? TransactionId { get; private set; } // شناسه تراکنش داخلی سیستم
        public DateTime? PaidAt { get; private set; } // فقط در صورتی که پرداخت موفق بوده مقدار دارد

        // Navigation Properties
        public ReservationEntity Reservation { get; private set; }

        public enum PaymentStatus
        {
            success,
            Failed,
            Refunded
        }
    }
}
