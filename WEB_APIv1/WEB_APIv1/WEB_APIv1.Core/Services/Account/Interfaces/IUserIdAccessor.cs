// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

namespace WEB_APIv1.Core.Services.Account
{
    public interface IUserIdAccessor
    {
        string? GetCurrentUserId();
    }
}
