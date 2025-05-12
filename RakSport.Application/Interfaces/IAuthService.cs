using RakSport.Application.Commands.Auth;
using RakSport.Application.DTOs.Auth;

namespace RakSport.Application.Interfaces
{
    public interface IAuthService
    {
        Task<SendOtpDto> SendOtpAsync(SendOtpCommand command);
        Task<LoginResponseDto> VerifyOtpAsync(VerifyOtpCommand command);
        Task<LoginResponseDto> LoginAsync(LoginCommand command);
        Task ForgotPasswordAsync(ForgotPasswordCommand command);
    }
}
