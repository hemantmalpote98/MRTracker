using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MRTracking.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Log the request
            var user = context.HttpContext.User;
            var path = context.HttpContext.Request.Path;

            if (!context.HttpContext.Request.RouteValues["controller"].Equals("Auth"))
            {
                // Check if the user is authenticated
                if (!user.Identity.IsAuthenticated)
                {
                    // Log unauthorized access
                    Console.WriteLine($"Unauthorized access attempt to {path}");
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // If roles are defined, check if the user has the required roles
                if (!string.IsNullOrEmpty(Roles) && !user.IsInRole(Roles))
                {
                    // Log role failure
                    Console.WriteLine($"User {user.Identity.Name} does not have the required role(s) for {path}");
                    context.Result = new ForbidResult();
                    return;
                }

                // Log successful authorization
                Console.WriteLine($"User {user.Identity.Name} authorized for {path}");
            }

           
        }
    }
}
