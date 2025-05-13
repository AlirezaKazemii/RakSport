namespace RakSport.Application.Commands.Auth.ForgotPassword
{
    public class ForgotPasswordCommand
    {
        public string PhoneNumber { get; set; }
        public string OtpCode { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
