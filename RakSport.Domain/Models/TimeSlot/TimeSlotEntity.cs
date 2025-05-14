using RakSport.Domain.Models.ReservationItem;
using RakSport.Domain.Models.SportHall;

namespace RakSport.Domain.Models.TimeSlot
{
    public class TimeSlotEntity //جدول سانس ها
    {
        public Guid Id { get; private set; }
        public Guid SportHallId { get; private set; }
        public DateTime Date { get; private set; }  // تاریخ برگزاری سانس
        public TimeSpan StartTime { get; private set; }   // زمان شروع
        public TimeSpan EndTime { get; private set; } // زمان پایان
        public Gender gender { get; private set; } // جنسیت سانس
        public decimal Price { get; private set; } // قیمت اختصاصی این سانس 
        public int Capacity { get; private set; } // تعداد ظرفیت کل سانس
        public int ReservedCount { get; private set; } // تعداد نفراتی که این سانس رو رزرو کرده‌اند
        public bool IsActive { get; private set; }  // آیا این سانس فعال و قابل رزرو است؟
        public bool IsReservedOut => ReservedCount >= Capacity; // آیا رزرو تکمیل شده؟

        // Navigation Properties
        public SportHallEntity SportHall { get; private set; }
        public ICollection<ReservationItemEntity> reservationItems { get; private set; }

        public enum Gender
        {
            Men,
            Women
        }

    }
}
