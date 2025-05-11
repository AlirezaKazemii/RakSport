using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RakSport.Application.Commands.Auth;
using RakSport.Application.DTOs.Auth;
using RakSport.Application.Interfaces;
using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Interfaces.OtpCodeInterface;
using RakSport.Domain.Models.OtpCode;
using RakSport.Domain.Models.User;

namespace RakSport.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IOtpCodeRepository _otpCodeRepository;

        public AuthService(UserManager<UserEntity>userManager, IOtpCodeRepository otpCodeRepository)
        {
            _userManager = userManager;
            _otpCodeRepository = otpCodeRepository;
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
            var otp = await _otpCodeRepository.VerifyAsync(command.PhoneNumber,command.OtpCode);

            if (otp is null)
                throw new Exception("کد معتبر نیست یا منقضی شده است");

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == command.PhoneNumber);

            if(user is null)
            {
                var newUser = new UserEntity(command.PhoneNumber);

                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                    throw new Exception("خطا در ساخت حساب کاربری");
            }

            otp.OtpIsUsed();

            // ساخت توکن

            return new LoginResponseDto()
            {
                Token = "Token", // Test
                ExpireAt = DateTime.Now // Test
            };
        }
    }
}
    