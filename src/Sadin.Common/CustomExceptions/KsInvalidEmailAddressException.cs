namespace Sadin.Common.CustomExceptions;

public sealed class KsInvalidEmailAddressException : Exception
{
    public KsInvalidEmailAddressException()
        : base("Invalid email address")
    {
        
    }
}