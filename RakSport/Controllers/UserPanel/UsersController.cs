using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.UserCommand;
using RakSport.Application.Interfaces;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers.UserPanel
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IValidator<UpdateUserInfoCommand> _UpdateUserCommandValidator;
        private readonly IValidator<ChangePasswordCommand> _ChangePasswordCommandValidator;

        public UsersController(IUnitOfWork unitOfWork, IUserService userService, IValidator<UpdateUserInfoCommand> UpdateUserCommandValidator, IValidator<ChangePasswordCommand> changePasswordCommandValidator)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _UpdateUserCommandValidator = UpdateUserCommandValidator;
            _ChangePasswordCommandValidator = changePasswordCommandValidator;
        }


        [HttpGet]
        [Route("Get-Current-User")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _userService.GetCurrentUserAsync(User);
            return Ok(new Message("Your account information has been received.", result));
        }

        [HttpPut]
        [Route("Update-User-Info")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoCommand command)
        {
            var validationResult =  _UpdateUserCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _userService.UpdateUserInfoAsync(User, command);

            await _unitOfWork.SaveChangesAsync();

            return Ok(new Message("Your information has been successfully updated.", result));
        }

        [HttpPut]
        [Route("Change-User-Password")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordCommand command)
        {
            var validationResult =  _ChangePasswordCommandValidator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _userService.ChangePasswordAsync(User, command);

           await _unitOfWork.SaveChangesAsync();

            return Ok(new Message("Password change was successful."));
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok(new Message("You Logout successfully."));
        }

    }
}
