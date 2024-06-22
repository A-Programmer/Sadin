namespace Sadin.Common.CustomExceptions;

public sealed class KsInvalidPhoneNumberException : KsException
{
    public KsInvalidPhoneNumberException(string? message = null)
        : base(message ?? "Invalid phone number")
    {
        
    }
}