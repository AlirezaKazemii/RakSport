namespace RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion
{
    public class CancelReservationCommand
    {
        public Guid ReservationId { get; set; }
        public List<Guid> ItemsIdsToCancel { get; set; }
    }
}
