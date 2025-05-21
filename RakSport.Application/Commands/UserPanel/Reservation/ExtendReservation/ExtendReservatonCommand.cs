namespace RakSport.Application.Commands.UserPanel.Reservation.ExtendReservation
{
    public class ExtendReservatonCommand
    {
        public Guid ReservationId { get; set; }
        public int MonthCount { get; set; }
    }
}
