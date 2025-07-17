using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSec = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Cashe Key
            string CacheKey = CreatCasheKey(context.HttpContext.Request);

            // Search For Value With Cache Key
            ICacheServices cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();
            var cacheValue = await cacheService.GetAsync(CacheKey);

            // Return Value If Not Null
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            // Return Value If Is Null , Invoke .Next
            var ExcutedContext = await next.Invoke();

            // Set Value With Cache Key
            if (ExcutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(CacheKey,result.Value ,timeToLive:  TimeSpan.FromSeconds(DurationInSec));
            }
        }

        private string CreatCasheKey(HttpRequest request)
        {
            //{{BaseUrl}}/api/Products?TypeId=20&BrandId=10

            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(Q=>Q.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();
        }
    }
}
