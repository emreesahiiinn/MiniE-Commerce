using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions;

public class ResultMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        context.Response.OnStarting(
            state =>
            {
                var httpContext = (HttpContext)state;
                var result = httpContext.Items["Result"] as IResult;

                if (result != null) httpContext.Response.StatusCode = result.StatusCode;

                return Task.CompletedTask;
            },
            context
        );

        await next(context);
    }
}