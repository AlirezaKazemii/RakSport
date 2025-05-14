using RakSport.Domain.Interfaces.GenericInterface;
using RakSport.Domain.Models.OtpCode;

namespace RakSport.Domain.Interfaces.Auth.OtpCodeInterface
{
    public interface IOtpCodeRepository : IGenericRepository<OtpCodeEntity>
    {
        Task<OtpCodeEntity?> VerifyAsync(string phoneNumber, string otpCode);
    }
}
