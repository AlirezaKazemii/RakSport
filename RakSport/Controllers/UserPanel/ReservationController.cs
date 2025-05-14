using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers.UserPanel
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationService _reservationService;

        public ReservationController(IUnitOfWork unitOfWork, IReservationService reservationService)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
        }

        [HttpGet]
        [Route("Get-Reservation-ById")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetReservationById([FromQuery] GetReservationByIdCommand getReservationByIdCommand)
        {
            var result = await _reservationService.GetReservationByIdAsync(User, getReservationByIdCommand);

            await _unitOfWork.SaveChangesAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("Get_list_of_all_reservations")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Getalistofallreservations()
        {
            throw new NotImplementedException();
        }

    }
}
