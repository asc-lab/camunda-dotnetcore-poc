using System.Linq;
using System.Security.Claims;

namespace HeroesForHire.Domain
{
    public static class UserExtensions
    {
        public static string CompanyCode(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "user-company")?.Value;
        }
      
    }
}