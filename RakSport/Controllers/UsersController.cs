using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserCommand;
using RakSport.Application.Interfaces;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValidator<UpdateUserInfoCommand> _UpdateUserCommandValidator;

        public UsersController(IUnitOfWork unitOfWork, IUserService userService, IValidator<UpdateUserInfoCommand> UpdateUserCommandValidator)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _UpdateUserCommandValidator = UpdateUserCommandValidator;
        }


        [HttpGet]
        [Route("Get-Current-User")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetCurrentUserAsync(User);
            return Ok(new ResponseHandler("Account information: ", result));
        }

        [HttpPut]
        [Route("Update-User-Info")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoCommand command)
        {
            var validationResult = await _UpdateUserCommandValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _userService.UpdateUserInfoAsync(User, command);
            return Ok(new ResponseHandler("Update information: ", result));
        }


        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(new ResponseHandler("You Logout successfully."));
        }
    }
}
