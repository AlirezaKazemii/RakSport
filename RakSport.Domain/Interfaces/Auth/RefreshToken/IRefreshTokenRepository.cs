using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.RefreshToken;

namespace RakSport.Domain.Interfaces.Auth.RefreshToken
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenEntity>
    {
        Task<RefreshTokenEntity?> GetValidTokenAsync(string token);
        Task MarkAsUsedAsync(RefreshTokenEntity refreshToken);
    }
}
