namespace Sadin.Common.CustomExceptions;

public sealed class KsParentHasChildrenException : KsException
{
    public KsParentHasChildrenException(string? message = null)
        : base(message ?? "The row that you are deleting has children, please remove children and then try again.")
    {
    }
}