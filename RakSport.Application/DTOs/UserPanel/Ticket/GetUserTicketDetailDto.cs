namespace RakSport.Application.DTOs.UserPanel.Ticket
{
    public class GetUserTicketDetailDto
    {
        public string Subject { get; set; }
        public string Status { get; set; }
        public DateTime CreateAt { get; set; }
        public List<TicketMessageDto> Messages { get; set; }
    }

    public class TicketMessageDto
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}
