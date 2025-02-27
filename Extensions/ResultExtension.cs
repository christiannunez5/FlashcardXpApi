using FlashcardXpApi.Common.Results;

namespace FlashcardXpApi.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToHttpResponse(this Result result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result);
            }

            return MapErrorResponse(result.Error, result);

        }

        public static IResult ToHttpResponse<T>(this ResultGeneric<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result);
            }

            return MapErrorResponse(result.Error, result);

        }

        private static IResult MapErrorResponse(Error error, object result)
        {
            return error.Code switch
            {
                ErrorTypeConstant.UNAUTHORIZED => Results.Json(result, statusCode: 401),
                ErrorTypeConstant.VALIDATION_ERROR => Results.BadRequest(result),
                ErrorTypeConstant.NOT_FOUND => Results.NotFound(result),
                ErrorTypeConstant.BAD_REQUEST => Results.BadRequest(result),
                ErrorTypeConstant.CONFLICT => Results.Conflict(result),
                ErrorTypeConstant.FORBIDDEN => Results.Json(result, statusCode: 403),
                _ => Results.Problem(detail: error.Message, statusCode: 500)
            };
        }
    }

    
}
