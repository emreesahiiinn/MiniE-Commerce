using System.Net;
using Autofac.Core;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Core.Extensions;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
    {
        httpContext.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Internal Server Error";

        switch (e)
        {
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = e.Message;
                break;
            case ArgumentNullException:
                statusCode = HttpStatusCode.BadRequest;
                message = e.Message;
                break;
            case ObjectDisposedException:
                statusCode = HttpStatusCode.BadRequest;
                message = e.Message;
                break;
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                message = e.Message;
                break;
            case NullReferenceException:
                statusCode = HttpStatusCode.BadRequest;
                message = "Resource Not Found";
                break;
            case InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                message = e.Message;
                break;
            case SecurityTokenException:
                statusCode = HttpStatusCode.Unauthorized;
                message = e.Message;
                break;
            case DependencyResolutionException:
                statusCode = HttpStatusCode.InternalServerError;
                message = e.Message;
                break;
            case TimeoutException:
                statusCode = HttpStatusCode.RequestTimeout;
                message = "Request Timed Out";
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = e.Message;
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;

        var result = new ErrorDetails
        {
            Status = false,
            StatusCode = (int)statusCode,
            Message = message
        };

        try
        {
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}