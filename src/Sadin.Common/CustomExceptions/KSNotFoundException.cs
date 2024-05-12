namespace Sadin.Common.CustomExceptions;

public class KsNotFoundException : Exception
{
    public KsNotFoundException(string name)
        : base($"{name} could not be found.")
    {
        
    }
}