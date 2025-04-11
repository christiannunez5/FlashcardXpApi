using Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions;

public static class ResultExtension
{
    public static IResult ToHttpResponse(this Result result)
    {
        return result.IsSuccess ? Results.Ok() : MapErrorResponse(result.Error);
    }
    
    public static IResult ToHttpResponse<T>(this Result<T> result)
    {
        return result.IsSuccess ? Results.Ok(result.Value) : MapErrorResponse(result.Error);
    }
    
    private static IResult MapErrorResponse(Error error)
    {
        return error.Code switch
        {
            ErrorTypeConstant.AUTHENTICATION_ERROR => Results.Json(error, statusCode: 401),
            ErrorTypeConstant.VALIDATION_ERROR => Results.BadRequest(error),
            ErrorTypeConstant.NOT_FOUND => Results.NotFound(error),
            ErrorTypeConstant.BAD_REQUEST => Results.BadRequest(error),
            ErrorTypeConstant.CONFLICT => Results.Conflict(error),
            ErrorTypeConstant.FORBIDDEN => Results.Json(error, statusCode: 403),
            _ => Results.Problem(detail: error.Message, statusCode: 500)
        };
    } 
}