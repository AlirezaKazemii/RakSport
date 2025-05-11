namespace RakSport.Application.Commands.Auth
{
    public class VerifyOtpCommand
    {
        public string PhoneNumber { get; set; }
        public string OtpCode { get; set; }
    }
}
