using JitAPI.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;

namespace JitAPI.Auth
{
    public static class HttpContextExtensions
    {

        /// <summary>
        /// Retrieves the User ID from the JWT claims.
        /// </summary>
        /// 
        public static string? GetUserId(this HttpContext context)
        {
            return context.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ??
                   context.User.FindFirst("sub")?.Value;
        }
    }
}
