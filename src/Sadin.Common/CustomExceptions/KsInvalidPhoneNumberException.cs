namespace Sadin.Common.CustomExceptions;

public sealed class KsInvalidPhoneNumberException : Exception
{
    public KsInvalidPhoneNumberException()
        : base("Invalid phone number")
    {
        
    }
}