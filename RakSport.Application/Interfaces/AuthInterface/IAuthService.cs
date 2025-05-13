using RakSport.Application.Commands.Auth.ForgotPassword;
using RakSport.Application.Commands.Auth.Login;
using RakSport.Application.Commands.Auth.RefreshToken;
using RakSport.Application.Commands.Auth.SendOtp;
using RakSport.Application.Commands.Auth.VerifyOtp;
using RakSport.Application.DTOs.Auth;

namespace RakSport.Application.Interfaces.AuthInterface
{
    public interface IAuthService
    {
        Task<SendOtpDto> SendOtpAsync(SendOtpCommand command);
        Task<LoginResponseDto> VerifyOtpAsync(VerifyOtpCommand command);
        Task ForgotPasswordAsync(ForgotPasswordCommand command);
        Task<LoginResponseDto> LoginAsync(LoginCommand command);
        Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenCommand command);

    }
}
