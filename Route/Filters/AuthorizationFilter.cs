using Microsoft.AspNetCore.Mvc.Filters;

namespace Route
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        // if u can :)
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}