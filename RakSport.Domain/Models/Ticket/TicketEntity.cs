using RakSport.Domain.Models.TicketMessage;
using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.Ticket
{
    public class TicketEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Subject { get; private set; }
        public TicketStatus Status { get; private set; }
        public DateTime CreateAt { get; private set; }

        // Navigation Properties
        public UserEntity User { get; private set; }
        public ICollection<TicketMessageEntity> TicketMessages { get; private set; }

        public enum TicketStatus
        {
            Open,
            Closed
        }

    }
}
