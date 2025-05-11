using Microsoft.AspNetCore.Identity;
using RakSport.Domain.Models.Notification;
using RakSport.Domain.Models.Reservation;
using RakSport.Domain.Models.Review;
using RakSport.Domain.Models.Ticket;

namespace RakSport.Domain.Models.User
{
    public class UserEntity : IdentityUser<Guid>
    {
        public string? FullName { get; private set; }
        public bool? Gender { get; private set; }
        public string? ShabaNumber { get; private set; }
        public bool IsBlocked { get; private set; }
        public string Role { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public DateTime? UpdateAt { get; private set; }

        // Navigation Properties
        public ICollection<ReservationEntity> Reservations { get; private set; }
        public ICollection<NotificationEntity> Notifications { get; private set; }
        public ICollection<TicketEntity> Tickets { get; private set; }
        public ICollection<ReviewEntity> Reviews { get; private set; }


        public UserEntity(string PhoneNumber)
        {
            Id = Guid.NewGuid();
            UserName = PhoneNumber;
            this.PhoneNumber = PhoneNumber;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
            Role = "user";
            IsBlocked = false;
        }



        

    }
}
