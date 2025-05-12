using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RakSport.Application.Commands.Auth;
using RakSport.Application.DTOs.Auth;
using RakSport.Application.Interfaces;
using RakSport.Application.Interfaces.ISmsService;
using RakSport.Application.Services.melipayamakService;
using RakSport.Domain.Interfaces.OtpCodeInterface;
using RakSport.Domain.Models.OtpCode;
using RakSport.Domain.Models.User;
using RakSport.Infrastructure.Smsserviceoptions;

namespace RakSport.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IOtpCodeRepository _otpCodeRepository;
        private readonly IJwtService _jwtService;
        //private readonly SmsServiceOptions _Options;
        private readonly IMeliPayamak _smsService;
        public AuthService(UserManager<UserEntity> userManager, IOtpCodeRepository otpCodeRepository, IJwtService jwtService, IOptions<SmsServiceOptions> options , IMeliPayamak smsService)
        {
            _userManager = userManager;
            _otpCodeRepository = otpCodeRepository;
            _jwtService = jwtService;
            _smsService = smsService;
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
                var result = await _userManager.CreateAsync(newUser,command.Password);

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
        public async Task<LoginResponseDto> LoginAsync(LoginCommand command)
        {
            var user = _userManager.Users.Where(u => !u.IsBlocked).FirstOrDefault(u => u.PhoneNumber == command.PhoneNumber);

            if (user is null)
                throw new Exception("User not found!");

            var checkPassword = await _userManager.CheckPasswordAsync(user, command.Password);

            if (!checkPassword)
                throw new Exception("Password is Incorrect.");

            var token = await _jwtService.GenerateToken(user.Id);

            return new LoginResponseDto()
            {
                Token = token,
                ExpireAt = DateTime.UtcNow.AddMinutes(15)
            };
        }

        public async Task ForgotPasswordAsync(ForgotPasswordCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.PhoneNumber); 

            if (user is null)
                throw new Exception("user not found");

            var otp = await _otpCodeRepository.VerifyAsync(command.PhoneNumber,command.OtpCode);

            if (otp is null)
                throw new Exception("Otp is not correct");

            if (command.NewPassword != command.RepatePassword)
                throw new Exception("Repeat password is not valid.");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasword = await _userManager.ResetPasswordAsync(user,resetToken,command.NewPassword);
        }
    }
}
