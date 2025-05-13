using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.RefreshToken;
using RakSport.Domain.Models.RefreshToken;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Genericrepository;

namespace RakSport.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshTokenEntity>, IRefreshTokenRepository
    {
        private readonly DataBaseContext _context;

        public RefreshTokenRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshTokenEntity?> GetValidTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == token && !r.IsUsed && !r.IsRevoked && r.ExpireAt > DateTime.Now);
        }

        public async Task MarkAsUsedAsync(RefreshTokenEntity refreshToken)
        {
            refreshToken.RefreshTokenIsUsed();
            _context.RefreshTokens.Update(refreshToken);
        }
    }
}