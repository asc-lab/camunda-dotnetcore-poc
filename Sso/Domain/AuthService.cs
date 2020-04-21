using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Sso.Domain
{
    public class AuthService
    {
        private readonly IUsersList users;
        private readonly AppSettings appSettings;

        public AuthService(IUsersList users, IOptions<AppSettings> appSettings)
        {
            this.users = users;
            this.appSettings = appSettings.Value;
        }

        public AuthResult Authenticate(string login, string pwd)
        {
            var user = users.FindByLogin(login);

            if (user == null)
                return null;

            if (!user.PasswordMatches(pwd))
                return null;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("sub", user.Login),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.UserType.ToString()),
                    new Claim("user-type", user.UserType.ToString()) 
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResult
            {
                Token = tokenHandler.WriteToken(token),
                User = user.Login,
                Expires = tokenDescriptor.Expires
            };
        }

        public User UserWithLogin(string login)
        {
            return users.FindByLogin(login);
        }
    }

    public class AuthResult
    {
        public string Token { get; set; }
        public string User { get; set; }
        public DateTime? Expires { get; set; }
    }
}