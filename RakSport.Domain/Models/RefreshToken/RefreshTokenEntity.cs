using RakSport.Domain.Models.User;

namespace RakSport.Domain.Models.RefreshToken
{
    public class RefreshTokenEntity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public bool IsUsed { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime CreateAt { get; private set; } = DateTime.UtcNow;

        //Navigation Properties
        public UserEntity User { get; private set; }


        public RefreshTokenEntity(Guid userId, string token, DateTime expireAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            ExpireAt = expireAt;
            CreateAt = DateTime.Now;
            IsUsed = false;
            IsRevoked = false;
        }

            
        public void RefreshTokenIsUsed()
        {
            IsUsed = true;
        }
    }
}
