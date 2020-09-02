using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMap.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TokenAuthAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ITokenRepository _tokenReposiroty = context.HttpContext.RequestServices.GetRequiredService<ITokenRepository>();
            string? token = context.HttpContext.Request.Query["token"];
            if (!_tokenReposiroty.isTokenExist(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
