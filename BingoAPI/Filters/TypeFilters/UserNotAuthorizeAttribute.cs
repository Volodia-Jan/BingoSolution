using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BingoAPI.Filters.TypeFilters;

public class UserNotAuthorizeAttribute : TypeFilterAttribute
{
    public UserNotAuthorizeAttribute() : base(typeof(UserNotAuthorize))
    {
    }

    private sealed class UserNotAuthorize : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
                context.Result = new StatusCodeResult(405);

            return Task.CompletedTask;
        }
    }
}
