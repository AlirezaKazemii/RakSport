using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RakSport.Application.Commands.UserCommand;
using RakSport.Application.DTOs.User;
using RakSport.Application.Interfaces;
using RakSport.Domain.Models.User;
using System.Security.Claims;

namespace RakSport.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetCurrentUserDto> GetCurrentUserAsync(ClaimsPrincipal User)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(currentUserId);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsBlocked);

            if (user is null)
                throw new Exception("User not found!");

            return new GetCurrentUserDto()
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                ShabaNumber = user.ShabaNumber,
                Gender = user.Gender
            };
        }

        public async Task<UpdateUserInfoDto> UpdateUserInfoAsync(ClaimsPrincipal User, UpdateUserInfoCommand command)
        {

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(loggedInUserId))
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUserId);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsBlocked);

            if (user is null)
                throw new Exception("User not found!");

            if (!string.IsNullOrEmpty(command.FullName))
                user.SetFullName(command.FullName);

            if (!string.IsNullOrEmpty(command.Email) && command.Email != user.Email)
                user.SetEmail(command.Email);

            if (!string.IsNullOrEmpty(command.ShabaNumber))
                user.SetShabaNumber(command.ShabaNumber);

            if (command.Gender.HasValue)
                user.SetGender(command.Gender.Value);

            user.SetUpdateTime();

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new Exception("Error updating user information");

            return new UpdateUserInfoDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender,
            };
        }

        public async Task<ChangeUserPasswordDto> ChangeUserPassword(ClaimsPrincipal User, ChangeUserPasswordCommand command)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(loggedInUserId))
                throw new UnauthorizedAccessException();

            var userId = Guid.Parse(loggedInUserId);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsBlocked);

            if (user is null)
                throw new Exception("User not found!");

            //var passwordValidator = new PasswordValidator<UserEntity>();
            //var validationResult = await passwordValidator.ValidateAsync(
            //    _userManager as UserManager<UserEntity>, user, command.NewPassword);

            //var removeResult = await _userManager.RemovePasswordAsync(user);
            //if (!removeResult.Succeeded)
            //    throw new Exception("خطا در حذف رمز قبلی: " + string.Join(", ", removeResult.Errors.Select(e => e.Description)));

            var addResult = await _userManager.AddPasswordAsync(user, command.NewPassword);
            if (!addResult.Succeeded)
                throw new Exception("خطا در افزودن رمز جدید: " + string.Join(", ", addResult.Errors.Select(e => e.Description)));

            user.SetUpdateTime();

            return new ChangeUserPasswordDto
            {
                Message = "رمز عبور با موفقیت تغییر کرد."
            };
        }
    }
}