using RakSport.Domain.Models.ContentSection;
using RakSport.Domain.Models.HallFeature;
using RakSport.Domain.Models.HallImage;
using RakSport.Domain.Models.RefundPolicy;
using RakSport.Domain.Models.Review;
using RakSport.Domain.Models.TimeSlot;

namespace RakSport.Domain.Models.SportHall
{
    public class SportHallEntity
    {
        public Guid Id { get; private set; }
        public Guid RefundPolicyId { get; private set; }
        public string Title { get; private set; }  // عنوان سالن
        public string Description { get; private set; } // توضیحات سالن
        public int Capacity { get; private set; } // ظرفیت نفرات سالن
        public int DurationMinutes { get; private set; } // مدت‌زمان هر سانس
        public decimal BasePrice { get; private set; } // قیمت پایه هر سانس
        public string? UsageTerms { get; private set; }  // شرایط استفاده
        public string? Details { get; private set; }  // مشخصات
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdateAt { get; private set; }
        public bool IsActive { get; private set; }
        public SportHallType Type { get; private set; }

        // Navigation Properties
        public ICollection<TimeSlotEntity> TimeSlots { get; private set; }
        public ICollection<HallFeatureEntity> Features { get; private set; }
        public ICollection<HallImageEntity> Images { get; private set; }
        public ICollection<ReviewEntity> reviews { get; private set; }
        public ICollection<ContentSectionEntity> ContentSectionEntities { get; private set; }
        public RefundPolicyEntity RefundPolicy { get; private set; }


        public SportHallEntity(Guid id, string title, Guid refundPolicyId,string description,int capacity, int durationMinutes, decimal basePrice, SportHallType type)
        {
            Id = id;
            Title = title;
            RefundPolicyId = refundPolicyId;
            Description = description;
            Capacity = capacity;
            DurationMinutes = durationMinutes;
            BasePrice = basePrice;
            CreatedAt = DateTime.Now;
            Type = type;
            IsActive = true;
        }

        protected SportHallEntity() { }

        public enum SportHallType
        {
            MartialArts,
            Futsal,
            Swimming
        }

        public void DeActivate()
        {
            IsActive = false;
        }

    }
}
