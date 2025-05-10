using RakSport.Domain.Models.SportHall;

namespace RakSport.Domain.Models.HallImage
{
    public class HallImageEntity
    {
        public Guid Id { get; private set; }
        public Guid SportHallId { get; private set; }
        public string Url { get; private set; }
        public string? AltText { get; private set; } // متن جایگزین تصویر برای دسترسی بهت
        public DateTime CreateAt { get; private set; }

        // Navigation Properties
        public SportHallEntity sportHall { get; private set; }
    }
}
