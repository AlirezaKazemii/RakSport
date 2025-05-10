using RakSport.Domain.Models.SportHall;
using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.Review
{
    public class ReviewEntity
    {
        public Guid Id { get; private set; }
        public Guid SportHallId { get; private set; }
        public Guid UserId { get; private set; }
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime CreateAt { get; private set; }

        // Navigation Properties
        public UserEntity User { get; private set; }
        public SportHallEntity sportHall { get; private set; }

    }
}
