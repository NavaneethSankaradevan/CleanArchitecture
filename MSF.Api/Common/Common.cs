using System;
using System.Security.Claims;

namespace MSF.Api
{
    public static class Common
    {
        public static string GetLoggedInUser(ClaimsPrincipal user)
        {
            try
            {
                return user.FindFirst(ClaimTypes.GivenName).Value;
            }
            catch { }
            return string.Empty;
        }
    }
}
