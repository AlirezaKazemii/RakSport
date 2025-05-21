using RakSport.Domain.Models.Ticket;

namespace RakSport.Application.Commands.UserPanel.Ticket.GetUserTicket
{
    public class GetUserTicketCommand
    {
        public string? Status { get; set; } // Null = All of Tickets


        public TicketEntity.TicketStatus? ParsedStatus()
        {
            if(string.IsNullOrWhiteSpace(Status))
                return null;

            switch (Status.ToLower())
            {
                case "open": 
                    return TicketEntity.TicketStatus.Open;
                case "closed":
                    return TicketEntity.TicketStatus.Closed;
                default:
                    return null;
            }
        }
    }
}
