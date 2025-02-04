using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeesTimeControl.Services
{
    public class JwtService
    {
        public string GenerateToken(string username)
        {
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5BF956B8D2846F1A71482627EDACF778356E099253F0EACCF6BEA49DA1EE310A6E071E0477A85D74D9E184C639477AC3DA7BA609A160F06BA0F1BB6A520CCD76"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5126",
                audience: "http://localhost:5126",
                claims: claim,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credential);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
