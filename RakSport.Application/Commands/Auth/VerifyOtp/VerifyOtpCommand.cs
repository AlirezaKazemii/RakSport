namespace RakSport.Application.Commands.Auth.VerifyOtp
{
    public class VerifyOtpCommand
    {
        public string PhoneNumber { get; set; }
        public string OtpCode { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }
    }
}
