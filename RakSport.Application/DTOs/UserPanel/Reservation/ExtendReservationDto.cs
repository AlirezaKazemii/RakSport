namespace RakSport.Application.DTOs.UserPanel.Reservation
{

    public class ExtendReservationDto
    {
        public Guid ReservationId { get; set; }
        public int SessionCount { get; set; }
        //public string StartDate { get; set; }
        //public string EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ReservationItemDto> ReservationItems { get; set; }
    }

    public class ReservationItemDto()
    {

        public string Date { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Price { get; set; }

    }

}
