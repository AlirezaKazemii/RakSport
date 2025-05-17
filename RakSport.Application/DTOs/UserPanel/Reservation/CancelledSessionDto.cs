namespace RakSport.Application.DTOs.UserPanel.Reservation
{
    public class CancelledSessionDto
    {
        public Guid SessionId { get; set; }
        public decimal OriginalPrice { get; set; }
        public int PenaltyPercentage { get; set; }
        public decimal RefundAmount { get; set; }
    }

    public class CancelReservationResultDto
    {
        public List<CancelledSessionDto> CancelledSessions { get; set; }
        public decimal TotalPenalty { get; set; }
        public decimal TotalRefund { get; set; }
    }
}
