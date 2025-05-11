using RakSport.Domain.Models.ContentSection;
using RakSport.Domain.Models.HallFeature;
using RakSport.Domain.Models.HallImage;
using RakSport.Domain.Models.Review;
using RakSport.Domain.Models.TimeSlot;

namespace RakSport.Domain.Models.SportHall
{
    public abstract class SportHallEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }  // عنوان سالن
        public string Description { get; private set; } // توضیحات سالن
        public int Capacity { get; private set; } // ظرفیت نفرات سالن
        public int DurationMinutes { get; private set; } // مدت‌زمان هر سانس
        public decimal BasePrice { get; private set; } // قیمت پایه هر سانس
        public string? UsageTerms { get; private set; }  // شرایط استفاده
        public string? Details { get; private set; }  // مشخصات
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }
        public bool IsActive { get; private set; } = true;

        // Navigation Properties
        public ICollection<TimeSlotEntity> TimeSlots { get; private set; }
        public ICollection<HallFeatureEntity> Features { get; private set; }
        public ICollection<HallImageEntity> Images { get; private set; }
        public ICollection<ReviewEntity> reviews { get; private set; }
        public ICollection<ContentSectionEntity> ContentSectionEntities { get; private set; }


        protected SportHallEntity()
        {
            
        }

    }
}
