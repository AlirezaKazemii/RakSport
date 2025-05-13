using RakSport.Application.Commands.UserPanel.User.ChangePassword;
using RakSport.Application.Commands.UserPanel.User.UpdateUser;
using RakSport.Application.DTOs.UserPanel.User;
using RakSport.Application.Shared;
using RakSport.Domain.Models.User;
using System.Security.Claims;

namespace RakSport.Application.Interfaces.UserPanel
{
    public interface IUserService
    {
        Task<GetCurrentUserDto> GetCurrentUserAsync(ClaimsPrincipal User);

        Task<UpdateUserInfoDto> UpdateUserInfoAsync(ClaimsPrincipal User, UpdateUserInfoCommand command);

        Task ChangePasswordAsync(ClaimsPrincipal User, ChangePasswordCommand command);
    }
}
