using System.Security.Principal;
using Microsoft.AspNetCore.Diagnostics;

namespace PersonRestAPI.Middleware;

public class GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandling> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception,
            "Could not process a request on Machine {MachineName}. TraceId:{TraceId}",
            Environment.MachineName, httpContext.TraceIdentifier);

        // Mapping
        // statuscode and title
        var (statusCode, title) = MapException(exception);
        
        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>()
            {
                { "traceId", httpContext.TraceIdentifier }
            }
        ).ExecuteAsync(httpContext);

        return true; // we stop the pipeline
    }

    private static (int statusCode, string title) MapException(Exception exception)
    {
        // switch (exception)
        // {
        //     case ArgumentNullException:
        //         break; 
        //     default:
        //         break;
        // }
        
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, "You made a mistake, fix it!"),
            Microsoft.AspNetCore.Http.BadHttpRequestException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "We made a mistake but we are working on it!")
        };
    }
    
}