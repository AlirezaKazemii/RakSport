namespace RakSport.Application.DTOs.UserPanel.Reservation
{
    public class GetAllReservationsDto
    {
        public string HallName { get; set; }
        public string HallImageUrl { get; set; }
        public float AverageRating { get; set; }
        public int TotalReviews { get; set; }

        public int TotalSessions { get; set; }
        public string SessionGenderType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public string Status { get; set; }
        public string TrackingCode { get; set; }

        public bool CanExtend { get; set; }
        public bool CanReview { get; set; }

    }
}
