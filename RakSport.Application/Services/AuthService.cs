using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RakSport.Application.Commands.Auth;
using RakSport.Application.DTOs.Auth;
using RakSport.Application.Interfaces;
using RakSport.Domain.Interfaces.OtpCodeInterface;
using RakSport.Domain.Models.OtpCode;
using RakSport.Domain.Models.User;

namespace RakSport.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IOtpCodeRepository _otpCodeRepository;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<UserEntity> userManager, IOtpCodeRepository otpCodeRepository, IJwtService jwtService)
        {
            _userManager = userManager;
            _otpCodeRepository = otpCodeRepository;
            _jwtService = jwtService;
        }

        public async Task<SendOtpDto> SendOtpAsync(SendOtpCommand command)
        {
            var code = new Random().Next(100000, 999999).ToString();

            var otp = new OtpCodeEntity(command.PhoneNumber, code);

            await _otpCodeRepository.AddAsync(otp);

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
                var newUser = new UserEntity(command.PhoneNumber);
                var result = await _userManager.CreateAsync(newUser);

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

            return new LoginResponseDto()
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(15)
            };
        }

    }
}
