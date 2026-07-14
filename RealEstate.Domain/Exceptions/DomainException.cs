namespace RealEstate.Domain.Exceptions;

public sealed class DomainException : Exception
{
    public DomainException(
        string message,
        Exception? innerEception = null)
        : base(message, innerEception)
    {
    }
}
