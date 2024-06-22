namespace Sadin.Common.CustomExceptions;

public sealed class KsNotFoundException : KsException
{
    public KsNotFoundException(string? message = null)
        : base(message ?? $"The entity could not be found.")
    {
        
    }
}