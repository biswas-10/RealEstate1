
using FluentValidation;
using RealEstate.Api.Contracts;
using RealEstate.Domain.Exceptions;

namespace RealEstate.Api.Middleware;

public sealed class DefaultExceptionMapper : IExceptionMapper
{
    public (int StatusCode, ApiResponse<object> Response) 
        Map(Exception exception)
    {
        // if (exception is DomainException)
        // {
        //     return (
        //         StatusCodes.Status400BadRequest,
        //         ApiResponse<object>.FailureResponse(exception.Message)
        //     );
        // }
        //
        // if (exception is KeyNotFoundException)
        // {
        //     return (
        //         StatusCodes.Status404NotFound,
        //         ApiResponse<object>.FailureResponse(exception.Message)
        //         );
        // }
        //
        // if (exception is UnauthorizedAccessException)
        // {
        //     return (
        //         StatusCodes.Status401Unauthorized,
        //         ApiResponse<object>.FailureResponse(exception.Message)
        //         );
        // }
        //
        // return (
        //     StatusCodes.Status500InternalServerError,
        //     ApiResponse<object>.FailureResponse(
        //         "An unexpected error occured.")
        // );

        return exception switch
        {
            DomainException => (
                StatusCodes.Status400BadRequest,
                ApiResponse<object>.FailureResponse(
                    exception.Message)
            ),

            KeyNotFoundException => (
                StatusCodes.Status404NotFound,
                ApiResponse<object>.FailureResponse(
                    exception.Message)
            ),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                ApiResponse<object>.FailureResponse(
                    exception.Message)
            ),

            ValidationException => (
                StatusCodes.Status400BadRequest,
                ApiResponse<object>.FailureResponse(
                    exception.Message)
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                ApiResponse<object>.FailureResponse(
                    "An unexpected error occured.")
            )
        };
    }
}