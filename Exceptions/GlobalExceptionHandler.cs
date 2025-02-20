using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlashcardXpApi.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, 
            CancellationToken cancellationToken)
        {
            var (statusCode, title) = MapException(exception);
                
            await Results.Problem(
                title: title,
                statusCode: statusCode
            ).ExecuteAsync(httpContext);

            return true;
        }

        public static (int statusCode, string title) MapException(Exception exception)
        {
            return exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                ValidationException => (StatusCodes.Status400BadRequest, exception.Message),
                NotAuthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
                ConflictException => (StatusCodes.Status409Conflict, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error.")
            };
        }
    }
}
