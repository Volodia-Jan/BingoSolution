using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BingoAPI.Filters.TypeFilters;

public class AuthorizeUserAttribute : TypeFilterAttribute
{
    public AuthorizeUserAttribute() : base(typeof(AuthorizeUserFilter))
    {
    }

    private sealed class AuthorizeUserFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity?.Name;

            if (string.IsNullOrEmpty(userName))
            {
                context.Result = new BadRequestObjectResult("Something went wrong please try again!");
            }

            return Task.CompletedTask;
        }
    }
}
