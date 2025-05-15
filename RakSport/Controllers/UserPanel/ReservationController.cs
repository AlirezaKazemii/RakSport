using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Commands.UserPanel.Reservation.GetAllReservations;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationById;
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
        private readonly IValidator<CreateReviewCommand> _createReviewCommandValidator;

        public ReservationController(
            IUnitOfWork unitOfWork,
            IReservationService reservationService,
            IValidator<CreateReviewCommand> CreateReviewCommandValidator)
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
            _createReviewCommandValidator = CreateReviewCommandValidator;
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

        [HttpPost]
        [Route("Create-Review")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
        {
            var validationResult = _createReviewCommandValidator.Validate(command);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _reservationService.CreateReviewAsunc(command, User);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Your comment was successfully submitted."));
        }

    }
}
