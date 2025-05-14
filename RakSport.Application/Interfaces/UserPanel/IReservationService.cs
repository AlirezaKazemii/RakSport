using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.DTOs.UserPanel.Reservation;
using RakSport.Domain.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RakSport.Application.Interfaces.UserPanel
{
    public interface IReservationService
    {
        Task<GetReservationByIdDto> GetReservationByIdAsync(ClaimsPrincipal User, GetReservationByIdCommand command);

    }
}
