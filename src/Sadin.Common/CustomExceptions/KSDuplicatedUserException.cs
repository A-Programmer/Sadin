namespace Sadin.Common.CustomExceptions;

public sealed class KsDuplicatedUserException: KsException
{
    public KsDuplicatedUserException(string message)
        : base(message)
    {
    }
}