namespace RakSport.Application.Commands.UserPanel.Reservation
{
    public class CreateReviewCommand
    {
        public Guid ReservationId { get; set; }
        public int CleanLinessRating { get; set; }
        public int PriceToQualityRating { get; set; }
        public int StaffBehaviorRating { get; set; }
        public int LockerRoomRating { get; set; }
        public int VentilationLightingRating { get; set; }
        public string Comment { get; set; }
    }
}
