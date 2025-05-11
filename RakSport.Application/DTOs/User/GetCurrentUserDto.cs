namespace RakSport.Application.DTOs.User
{
    public class GetCurrentUserDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ShabaNumber { get; set; }
        public bool? Gender { get; set; }
    }
}
