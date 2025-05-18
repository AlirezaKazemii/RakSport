namespace RakSport.Application.DTOs.UserPanel.Reservation
{
    public class ReservationSessionsDto
    {
        public DateTime Date { get; set; }
        public string TimeRange { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

    }

    public class ReservationSessionsResultDto
    {
        public int TotalSessions { get; set; }
        public int CompletedSessions { get; set; }
        public int RemainingSessions { get; set; }
        public List<ReservationSessionsDto> Sessions { get; set; }
    }
}
