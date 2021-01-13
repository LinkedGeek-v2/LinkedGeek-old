using System.Security.Claims;
using System.Security.Principal;

namespace GroupProject.Models
{
    public static class IdentityExtensions
    {
        public static bool IsDeveloper(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("IsDeveloper");
            return claim.Value == "True";
        }
    }
}