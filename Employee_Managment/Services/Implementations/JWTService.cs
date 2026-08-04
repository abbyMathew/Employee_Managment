using Employee_Managment.Models;
using Employee_Managment.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Employee_Managment.Services.Implementations
{
    public class JWTService:IJwtService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            //var key = System.Text.Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var key = _configuration["Jwt:Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var SigningCredentials= new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {new Claim(ClaimTypes.Name, user.UserName),new Claim(ClaimTypes.Role, user.Role)};

            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],audience: _configuration["Jwt:Audience"],
            claims: claims,expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

            //var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            //{
            //    Subject = new System.Security.Claims.ClaimsIdentity(new[]
            //    {
            //        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.UserName),
            //        new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.Role)
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(1),
            //    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);
        }
    }
}
