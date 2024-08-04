using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendRestAPI.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;

        public TokenService(IConfiguration configuration)
        {
            _jwtKey = configuration["Jwt:Key"]!;
            _jwtIssuer = configuration["Jwt:Issuer"]!;
            _jwtAudience = configuration["Jwt:Audience"]!;
        }

        public CookieOptions GetTokenCookieOptions()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(Consts.TokenValidTimeInMinutes)
            };
        }

        public string GenerateJwtToken(string username)
        {
            // set fields for token
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username)
            };

            // prepare signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // generate token
            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Consts.TokenValidTimeInMinutes),
                signingCredentials: creds);

            // serialize token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
