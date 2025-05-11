using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

            if(string.IsNullOrEmpty(currentUserId))
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
    }
}
