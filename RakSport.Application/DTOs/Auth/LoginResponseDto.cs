namespace RakSport.Application.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
