using Microsoft.EntityFrameworkCore;
using RakSport.Domain.Interfaces.OtpCodeInterface;
using RakSport.Domain.Models.OtpCode;
using RakSport.Infrastructure.DbContext;
using RakSport.Infrastructure.Genericrepository;

namespace RakSport.Infrastructure.Repositories
{
    public class OtpCodeRepository : GenericRepository<OtpCodeEntity>, IOtpCodeRepository
    {
        private readonly DataBaseContext _context;

        public OtpCodeRepository(DataBaseContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<OtpCodeEntity?> VerifyAsync(string phoneNumber, string otpCode)
        {
            return await _context.OtpCodes.Where(o => o.PhoneNumber == phoneNumber && o.Code == otpCode && !o.IsUsed && o.ExpireAt > DateTime.Now)
                .OrderByDescending(o => o.CreateAt)
                .FirstOrDefaultAsync();
        }
    }
}
