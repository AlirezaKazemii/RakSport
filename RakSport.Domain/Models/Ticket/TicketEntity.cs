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

        public TicketEntity(Guid userId, string subject)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Subject = subject;
            CreateAt = DateTime.Now;
            Status = TicketStatus.Open;
            TicketMessages = new List<TicketMessageEntity>();
        }
            
        public enum TicketStatus
        {
            Open,
            Closed
        }


        public void Close()
        {
            Status = TicketStatus.Closed;
        }

    }
}
