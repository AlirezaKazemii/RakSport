namespace RakSport.Domain.Models.OtpCode
{
    public class OtpCodeEntity
    {
        public Guid Id { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Code { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public bool IsUsed { get; private set; }
        public DateTime? OtpGeneratedAt { get; private set; }
        public DateTime CreateAt { get; private set; } = DateTime.Now;


        public OtpCodeEntity(string phoneNumber, string? code)
        {
            Id = Guid.NewGuid();
            PhoneNumber = phoneNumber;
            Code = code;
            CreateAt = DateTime.Now;
            ExpireAt = DateTime.Now.AddMinutes(2);
            IsUsed = false;
        }

        public void OtpIsUsed()
        {
            IsUsed = true;
        }

        public void GenerateOtp()
        {
            var random = new Random();
            Code = random.Next(100000, 999999).ToString();
            OtpGeneratedAt = DateTime.Now;
        }


        //public void ClearOtp()
        //{
        //    Code = null;
        //    OtpGeneratedAt = null;
        //}

    }
}
