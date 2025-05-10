using RakSport.Domain.Models.Ticket;

namespace RakSport.Domain.Models.TicketMessage
{
    public class TicketMessageEntity
    {
        public Guid Id { get; private set; }
        public Guid TicketId { get; private set; }
        public SenderType Sender { get; private set; }
        public string Message { get; private set; }
        public DateTime SentAt { get; private set; }

        // Navigation Properties
        public TicketEntity Ticket { get; private set; }

        public enum SenderType
        {
            User,
            Admin
        }

    }
}
