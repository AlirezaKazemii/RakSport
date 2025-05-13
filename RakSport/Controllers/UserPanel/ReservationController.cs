using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers.UserPanel
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("Get-Reservation-ById")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetReservationById()
        {
            throw new NotImplementedException();
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
