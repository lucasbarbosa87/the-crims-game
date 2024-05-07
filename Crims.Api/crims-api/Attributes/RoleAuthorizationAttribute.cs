using Crims.Data.Dtos;
using crims_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace crims_api.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RoleAuthorizationAttribute : TypeFilterAttribute
    {
        public RoleAuthorizationAttribute(string role) : base(typeof(RoleAuthorizationFilter))
        {
            Arguments = [role];
        }
    }

    public class RoleAuthorizationFilter(string role) : IAuthorizationFilter
    {
        private readonly string _role = role;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated || _role != context.HttpContext.User.FindFirst(ClaimTypes.Role).Value)
            {
                var response = new ApiErrorDto("Sem acesso", "00");
                context.Result = new ForbiddenObjectResult(response);
            }
        }
    }
}
