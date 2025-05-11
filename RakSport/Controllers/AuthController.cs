using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.Auth;
using RakSport.Application.Interfaces;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IValidator<SendOtpCommand> _sendOtpCommandValidator;
        private readonly IValidator<VerifyOtpCommand> _verifyOtpCommandValidator;

        public AuthController(
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IValidator<SendOtpCommand> SendOtpCommandValidator,
            IValidator<VerifyOtpCommand> VerifyOtpCommandValidator)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _sendOtpCommandValidator = SendOtpCommandValidator;
            _verifyOtpCommandValidator = VerifyOtpCommandValidator;
        }


        [HttpPost]
        [Route("Send-OtpCode")]
        public async Task<IActionResult> SendOtpCode([FromBody] SendOtpCommand command)
        {
            var validationResult = await _sendOtpCommandValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var Result = await _authService.SendOtpAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseHandler("OtpCode: ", Result));
        }


        [HttpPost]
        [Route("Verify-OtpCode")]
        public async Task<IActionResult> VerifyOtpCode([FromBody] VerifyOtpCommand command)
        {
            var validationResult = await _verifyOtpCommandValidator.ValidateAsync(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _authService.VerifyOtpAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new ResponseHandler("Token: ", result));
        }



    }
}
