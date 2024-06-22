namespace Sadin.Common.CustomExceptions;

public sealed class KsAuthenticationException : KsException
{
    public KsAuthenticationException(string message)
        : base(message)
    {

    }
}