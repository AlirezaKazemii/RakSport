using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion;
using RakSport.Application.Commands.UserPanel.Reservation.ExtendReservation;
using RakSport.Application.Commands.UserPanel.Reservation.GetAllReservations;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationById;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationSessions;
using RakSport.Application.DTOs.UserPanel.Reservation;
using System.Security.Claims;

namespace RakSport.Application.Interfaces.UserPanel
{
    public interface IReservationService
    {
        Task<GetReservationByIdDto> GetReservationByIdAsync(ClaimsPrincipal User, GetReservationByIdCommand command);
        Task<List<GetAllReservationsDto>> GetAllAsync(GetAllReservationsCommand command, ClaimsPrincipal User);
        Task CreateReviewAsunc(CreateReviewCommand command, ClaimsPrincipal User);
        Task<CancelReservationResultDto> CancelReservationAsync(CancelReservationCommand command, ClaimsPrincipal User);
        Task<ReservationSessionsResultDto> GetReservationSessionsAsync(GetReservationSessionsCommand command, ClaimsPrincipal User);
        Task<ExtendReservationDto> ExtendReservationAsync(ExtendReservatonCommand command, ClaimsPrincipal User);
    }
}
