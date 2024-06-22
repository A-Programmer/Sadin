namespace Sadin.Common.CustomExceptions;

public abstract class KsException : Exception
{
    private const string DefaultMessage = "An error occured.";
    
    public KsException(string? message = null)
        : base(message ?? DefaultMessage)
    {
    }
}