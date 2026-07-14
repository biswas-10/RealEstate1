namespace RealEstate.Domain.Common;

public sealed record Error(
    string Code,
    string Message)
{
    public static readonly Error None =
        new(string.Empty, string.Empty);
    
    public static Error Validation(
        string code,
        string message)
    {
        return new Error(code, message);
    }

    public static Error NotFound(
        string code,
        string message)
    {
        return new Error(code, message);
    }

    public static Error Unauthorized(
        string code,
        string message)
    {
        return new Error(code, message);
    }

    public static Error Conflict(
        string code,
        string message)
    {
        return new Error(code, message);
    }

    public static Error Unexpected(
        string code,
        string message)
    {
        return new Error(code, message);
    }
}