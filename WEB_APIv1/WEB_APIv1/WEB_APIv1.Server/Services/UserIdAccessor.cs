// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using System.Security.Claims;
using WEB_APIv1.Core.Services.Account;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace WEB_APIv1.Server.Services
{
    public class UserIdAccessor(IHttpContextAccessor httpContextAccessor) : IUserIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? GetCurrentUserId() => _httpContextAccessor.HttpContext?.User.FindFirstValue(Claims.Subject);
    }

    public class SystemUserIdAccessor : IUserIdAccessor
    {
        private readonly string? id;

        private SystemUserIdAccessor(string? id) => this.id = id;

        public string? GetCurrentUserId() => id;

        public static SystemUserIdAccessor GetNewAccessor(string? id = "SYSTEM") => new(id);
    }
}
