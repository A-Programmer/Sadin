namespace Sadin.Common.CustomExceptions;

public sealed class KsInvalidEmailAddressException : KsException
{
    public KsInvalidEmailAddressException(string? message = null)
        : base(message ?? "Invalid email address")
    {
        
    }
}