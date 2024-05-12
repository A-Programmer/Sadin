namespace Sadin.Common.CustomExceptions;

public class KsAuthenticationException : Exception
{
    public KsAuthenticationException(string message)
        : base(message)
    {

    }
}