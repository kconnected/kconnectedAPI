using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using kconnected.API.Data;
using Microsoft.IdentityModel.Tokens;

namespace kconnected.API.Utilities
{

    public class JWTAuthenticationManager : IAuthenticationManager
    {
        private readonly string _key;

        private readonly kconnectedAPIDbContext _dbContext;


        public JWTAuthenticationManager(kconnectedAPIDbContext dbContext)
        {
            _key = "C1CF4B7DC4C4175B6618DE4F55CA4";
            _dbContext = dbContext;
        }

        public string Authenticate(string email, string password)
        {
            if (!_dbContext.Users.Any(u => u.Email == email && SecurePasswordHasher.Verify(password, u.Password)))
                return null;

            var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role,"User"),
                    new Claim("Id",user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Surname,user.Surname),
                    new Claim("UserName",user.UserName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim("RegistrationDate",user.RegistrationDate.ToString())
                    

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                         SecurityAlgorithms.HmacSha256Signature),
                Issuer = "kconnected",
                Audience = "User"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}