using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.Notification
{
    public class NotificationEntity
    {
        public Guid Id { get; private set; }
        public Guid? UserId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public bool IsSeen { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public NotificationType Type { get; private set; }


        // Navigation Properties
        public UserEntity? User { get; private set; } // هم اعلان شخصی داریم هم عمومی 

        public enum NotificationType
        {
            Email,
            SMS,
            InApp
        }


    }
}
