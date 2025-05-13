using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RakSport.Application.Commands.Auth;
using RakSport.Application.Commands.Auth.ForgotPassword;
using RakSport.Application.Commands.Auth.Login;
using RakSport.Application.Commands.Auth.RefreshToken;
using RakSport.Application.Commands.Auth.SendOtp;
using RakSport.Application.Commands.Auth.VerifyOtp;
using RakSport.Application.Interfaces.AuthInterface;
using RakSport.Application.Shared;
using RakSport.Domain.Interfaces.UnitOfWorkInterface;

namespace RakSport.API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IValidator<SendOtpCommand> _sendOtpCommandValidator;
        private readonly IValidator<VerifyOtpCommand> _verifyOtpCommandValidator;
        private readonly IValidator<ForgotPasswordCommand> _forgotPasswordCommandValidator;
        private readonly IValidator<LoginCommand> _loginCommandValidator;
        private readonly IValidator<RefreshTokenCommand> _refreshTokenCommandValidator;

        public AuthController(
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IValidator<SendOtpCommand> SendOtpCommandValidator,
            IValidator<VerifyOtpCommand> VerifyOtpCommandValidator,
            IValidator<ForgotPasswordCommand> ForgotPasswordCommandValidator,
            IValidator<LoginCommand> LoginCommandValidator,
            IValidator<RefreshTokenCommand> RefreshTokenCommandValidator)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _sendOtpCommandValidator = SendOtpCommandValidator;
            _verifyOtpCommandValidator = VerifyOtpCommandValidator;
            _forgotPasswordCommandValidator = ForgotPasswordCommandValidator;
            _loginCommandValidator = LoginCommandValidator;
            _refreshTokenCommandValidator = RefreshTokenCommandValidator;
        }


        [HttpPost]
        [Route("Send-OtpCode")]
        public async Task<IActionResult> SendOtpCode([FromBody] SendOtpCommand command)
        {
            var validationResult = _sendOtpCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var Result = await _authService.SendOtpAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("OTP code was successfully generated.", Result));
        }


        [HttpPost]
        [Route("Verify-OtpCode")]
        public async Task<IActionResult> VerifyOtpCode([FromBody] VerifyOtpCommand command)
        {
            var validationResult = _verifyOtpCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _authService.VerifyOtpAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Login was successful.", result));
        }


        [HttpPost]
        [Route("Forgot-Password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var validationResult = _forgotPasswordCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            await _authService.ForgotPasswordAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Your password has been changed successfully."));
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var validationResult = _loginCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _authService.LoginAsync(command);
            return Ok(new Message("Login was successful.", result));
        }


        [HttpPost]
        [Route("Refresh-Token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var validationResult = _refreshTokenCommandValidator.Validate(command);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await _authService.RefreshTokenAsync(command);
            await _unitOfWork.SaveChangesAsync();
            return Ok(new Message("Refresh token was successfully generated.", result));
        }

    }
}
