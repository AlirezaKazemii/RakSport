using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Application.Shared;
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
        [Route("GetAll-Reservations")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAllReservations([FromQuery] GetAllReservationsCommand command)
        {
            var result = await _reservationService.GetAllAsync(command, User);
            return Ok(new Message("Reservation list received successfully.", result));
        }

    }
}
