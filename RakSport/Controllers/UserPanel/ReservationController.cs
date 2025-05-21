using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserPanel.Reservation;
using RakSport.Application.Commands.UserPanel.Reservation.CancelReservtaion;
using RakSport.Application.Commands.UserPanel.Reservation.ExtendReservation;
using RakSport.Application.Commands.UserPanel.Reservation.GetAllReservations;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationById;
using RakSport.Application.Commands.UserPanel.Reservation.GetReservationSessions;
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
        private readonly IValidator<CancelReservationCommand> _cancelReservationCommandValidator;
        private readonly IValidator<GetReservationSessionsCommand> _getReservationSessionsCommandValidator;
        private readonly IValidator<ExtendReservatonCommand> _extendReservatonCommandValidator;

        public ReservationController(
            IUnitOfWork unitOfWork,
            IReservationService reservationService,
            IValidator<CreateReviewCommand> CreateReviewCommandValidator,
            IValidator<CancelReservationCommand> CancelReservationCommandValidator,
            IValidator<GetReservationSessionsCommand> GetReservationSessionsCommandValidator,
            IValidator<ExtendReservatonCommand> ExtendReservatonCommandValidator
            )
        {
            _unitOfWork = unitOfWork;
            _reservationService = reservationService;
            _createReviewCommandValidator = CreateReviewCommandValidator;
            _cancelReservationCommandValidator = CancelReservationCommandValidator;
            _getReservationSessionsCommandValidator = GetReservationSessionsCommandValidator;
            _extendReservatonCommandValidator = ExtendReservatonCommandValidator;
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


        [HttpPost]
        [Route("Cancel-Reservation")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CancelReservation([FromBody] CancelReservationCommand command)
        {
            var validationResult = _cancelReservationCommandValidator.Validate(command);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _reservationService.CancelReservationAsync(command, User);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Your reservation has been successfully canceled.", result));
        }


        [HttpGet]
        [Route("Get-Sessions")]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> GetSessions([FromQuery] GetReservationSessionsCommand command)
        {
            var validationResult = _getReservationSessionsCommandValidator.Validate(command);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _reservationService.GetReservationSessionsAsync(command, User);
            return Ok(new Message("The list of Reserved sessions was successfully received.", result));
        }


        [HttpPost]
        [Route("Extend-Reservation")]
        [Authorize(Roles = "user")] 
        public async Task<IActionResult> ExtendReservation([FromBody] ExtendReservatonCommand command)
        {
            var validationResult = _extendReservatonCommandValidator.Validate(command);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _reservationService.ExtendReservationAsync(command, User);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Reservation extension successfully completed.", result));
        }
    }
}
