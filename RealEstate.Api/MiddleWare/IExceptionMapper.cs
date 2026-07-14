using RealEstate.Api.Contracts;

namespace RealEstate.Api.Middleware;

public interface IExceptionMapper
{
    (int StatusCode, ApiResponse<object> Response) Map(Exception exception);
}