using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RakSport.Application.Commands.Auth.ForgotPassword;
using RakSport.Application.Commands.Auth.Login;
using RakSport.Application.Commands.Auth.RefreshToken;
using RakSport.Application.Commands.Auth.SendOtp;
using RakSport.Application.Commands.Auth.VerifyOtp;
using RakSport.Application.DTOs.Auth;
using RakSport.Application.Interfaces.Auth;
using RakSport.Domain.Interfaces.Auth.OtpCodeInterface;
using RakSport.Domain.Interfaces.Auth.RefreshToken;
using RakSport.Domain.Models.OtpCode;
using RakSport.Domain.Models.RefreshToken;
using RakSport.Domain.Models.User;
using RakSport.Infrastructure.Smsserviceoptions;
using System.Security.Cryptography;

namespace RakSport.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IOtpCodeRepository _otpCodeRepository;
        private readonly IJwtService _jwtService;
        //private readonly SmsServiceOptions _Options;
        private readonly IMeliPayamak _smsService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(UserManager<UserEntity> userManager, IOtpCodeRepository otpCodeRepository, IJwtService jwtService, IOptions<SmsServiceOptions> options, IMeliPayamak smsService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _otpCodeRepository = otpCodeRepository;
            _jwtService = jwtService;
            _smsService = smsService;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public async Task<SendOtpDto> SendOtpAsync(SendOtpCommand command)
        {
            var code = new Random().Next(100000, 999999).ToString();

            var tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

            var expireTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddMinutes(3), tehranTimeZone);

            var otp = new OtpCodeEntity(command.PhoneNumber, code, expireTime);

            //await _smsService.SendByBaseNumberAsync(code, command.PhoneNumber, 39885);

            await _otpCodeRepository.AddAsync(otp);

            Console.WriteLine($"{command.PhoneNumber} : {code}");

            return new SendOtpDto() { OtpCode = otp.Code };
        }
        public async Task<LoginResponseDto> VerifyOtpAsync(VerifyOtpCommand command)
        {
            var otp = await _otpCodeRepository.VerifyAsync(command.PhoneNumber, command.OtpCode);

            if (otp is null)
                throw new Exception("کد معتبر نیست یا منقضی شده است");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == command.PhoneNumber);

            if (user is null)
            {
                if (string.IsNullOrEmpty(command.Password) && string.IsNullOrEmpty(command.RepeatPassword))
                    throw new Exception("Password and password repeat are required for register.");

                if (command.Password != command.RepeatPassword)
                    throw new Exception("Reapeat password is not Valid");

                var newUser = new UserEntity(command.PhoneNumber);
                var result = await _userManager.CreateAsync(newUser, command.Password);

                if (!result.Succeeded)
                    throw new Exception("خطا در ساخت حساب کاربری");

                user = newUser;
            }

            otp.OtpIsUsed();
            await _otpCodeRepository.UpdateAsync(otp);

            user.setLastLogin();
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new Exception("خطا در به‌روزرسانی اطلاعات کاربر");

            var token = await _jwtService.GenerateToken(user.Id);

            // Generate Refresh Token
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var refreshTokenString = Convert.ToBase64String(bytes);

            var refreshToken = new RefreshTokenEntity(user.Id, refreshTokenString, DateTime.UtcNow.AddDays(7));
            await _refreshTokenRepository.AddAsync(refreshToken);

            return new LoginResponseDto()
            {
                Token = token,
                ExpireAt = DateTime.Now.AddMinutes(15),
                RefreshToken = refreshToken.Token
            };
        }
        public async Task ForgotPasswordAsync(ForgotPasswordCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.PhoneNumber);

            if (user is null)
                throw new Exception("user not found");

            var otp = await _otpCodeRepository.VerifyAsync(command.PhoneNumber, command.OtpCode);

            if (otp is null)
                throw new Exception("Otp is not correct");

            if (command.NewPassword != command.RepeatPassword)
                throw new Exception("Repeat password is not valid.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasword = await _userManager.ResetPasswordAsync(user, resetToken, command.NewPassword);
        }
        public async Task<LoginResponseDto> LoginAsync(LoginCommand command)
        {
            var user = _userManager.Users.Where(u => !u.IsBlocked).FirstOrDefault(u => u.PhoneNumber == command.PhoneNumber);

            if (user is null)
                throw new Exception("User not found!");

            var checkPassword = await _userManager.CheckPasswordAsync(user, command.Password);

            if (!checkPassword)
                throw new Exception("Password is Incorrect.");

            var token = await _jwtService.GenerateToken(user.Id);

            // Generate Refresh Token
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var refreshTokenString = Convert.ToBase64String(bytes);

            var refreshToken = new RefreshTokenEntity(user.Id, refreshTokenString, DateTime.UtcNow.AddDays(7));
            await _refreshTokenRepository.AddAsync(refreshToken);

            return new LoginResponseDto()
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(15),
                RefreshToken = refreshToken.Token
            };
        }
        public async Task<RefreshTokenDto> RefreshTokenAsync(RefreshTokenCommand command)
        {
            var refreshToken = await _refreshTokenRepository.GetValidTokenAsync(command.RefreshToken);

            if (refreshToken is null)
                throw new Exception("Refresh token is invalid or expired.");

            await _refreshTokenRepository.MarkAsUsedAsync(refreshToken);

            var accessToken = await _jwtService.GenerateToken(refreshToken.UserId);

            // Generate New Refresh Token
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var newToken = Convert.ToBase64String(bytes);

            var newRefreshToken = new RefreshTokenEntity(refreshToken.UserId, newToken, DateTime.Now.AddDays(7));

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return new RefreshTokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = newToken,
            };
        }
    }
}
