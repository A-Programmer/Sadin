namespace Sadin.Common.CustomExceptions;

public class KsNotFoundException : Exception
{
    public KsNotFoundException(string name)
        : base($"The entity, {name} could not be found.")
    {
        
    }
}