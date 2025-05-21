using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicket;
using RakSport.Application.Commands.UserPanel.Ticket.GetUserTicketDetail;
using RakSport.Application.Interfaces.UserPanel;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers.UserPanel
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetUserTicketDetailCommand> _getUserTicketDetailCommandValidator;

        public TicketsController(
            ITicketService ticketService,
            IUnitOfWork unitOfWork,
            IValidator<GetUserTicketDetailCommand> GetUserTicketDetailCommandValidator
            )
        {
            _ticketService = ticketService;
            _unitOfWork = unitOfWork;
            _getUserTicketDetailCommandValidator = GetUserTicketDetailCommandValidator;
        }


        [HttpGet]
        [Route("GetAll_Tickets")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAllTickets([FromQuery] GetUserTicketCommand command)
        {
            var result = await _ticketService.GetUserTecketsAsync(command, User);
            return Ok(new Message("Tickets received successfully.", result));
        }


        [HttpGet]
        [Route("Get_Ticket_Details")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTicketDetails([FromQuery] GetUserTicketDetailCommand command)
        {
            var validationResult = _getUserTicketDetailCommandValidator.Validate(command);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _ticketService.GetUserTicketDetailAsync(command, User);
            return Ok(new Message("User ticket details successfully received.", result));
        }
    }
}
