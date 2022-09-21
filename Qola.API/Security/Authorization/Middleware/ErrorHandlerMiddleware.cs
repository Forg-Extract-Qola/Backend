using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Qola.API.Security.Exceptions;

namespace Qola.API.Security.Authorization.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = MediaTypeNames.Application.Json;

            switch (ex)
            {
                case AppExceptions e:
                    response.StatusCode= (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(new { error = ex.Message });
            await response.WriteAsync(result);
        }
    }
}