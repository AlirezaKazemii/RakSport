using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.DTOs.UserPanel.Reservation;
using System.Security.Claims;

namespace RakSport.Application.Interfaces.UserPanel
{
    public interface IReservationService
    {




        Task<List<GetAllReservationsDto>> GetAllAsync(GetAllReservationsCommand command, ClaimsPrincipal User);
    }
}
