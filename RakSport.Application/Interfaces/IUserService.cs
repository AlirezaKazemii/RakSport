using RakSport.Application.Commands.UserCommand;
using RakSport.Application.DTOs.User;
using System.Security.Claims;

namespace RakSport.Application.Interfaces
{
    public interface IUserService
    {
        Task<GetCurrentUserDto> GetCurrentUserAsync(ClaimsPrincipal User);

        Task<UpdateUserInfoDto> UpdateUserInfoAsync(ClaimsPrincipal User, UpdateUserInfoCommand command);
    }
}
