using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RakSport.Application.Interfaces.Auth;
using RakSport.Domain.Models.User;
using RakSport.Infrastructure.jwtsettings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RakSport.Application.Services.Auth
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly JwtSettings _jwtSettings;

        public JwtService(UserManager<UserEntity> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> GenerateToken(Guid userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var user = await _userManager.FindByIdAsync(userid.ToString());

            if (user == null)
                throw new Exception("User Not Found");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userid.ToString()),

                new Claim(ClaimTypes.Role,user.Role),

                new Claim(ClaimTypes.Name,user.UserName)
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddMinutes(15),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
