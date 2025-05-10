using RakSport.Domain.Models.SportHall;

namespace RakSport.Domain.Models.HallFeature
{
    public class HallFeatureEntity
    {
        public Guid Id { get; private set; }
        public Guid SportHallId { get; private set; }
        public FeatureType featureType { get; private set; }

        // Navigation Properties
        public SportHallEntity SportHall { get; private set; }


        public enum FeatureType
        {
            Cafe = 1,          // کافه
            Shower = 2,        // حمام
            Locker = 3,        // رختکن
            AC = 4,            // تهویه مطبوع
            Lightin = 5,       // نورپردازی قوی
            Audience = 6,      // جایگاه تماشاگر
            ColdWater = 7,     // آبسردکن
            Parking = 8,       // پارکینگ
            CoolingSystem = 9  // سیستم سرمایش
        }

    }
}
