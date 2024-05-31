namespace Sadin.Common.CustomExceptions;

public sealed class KsParentHasChildrenException : Exception
{
    private const string DefaultMessage =
        "The row that you are deleting has children, please remove children and then try again.";
    public KsParentHasChildrenException(string? message = null)
        : base(
            string.IsNullOrEmpty(message) 
            ?  DefaultMessage
            : message)
    {
    }
}