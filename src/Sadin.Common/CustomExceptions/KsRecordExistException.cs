namespace Sadin.Common.CustomExceptions;

public sealed class KsRecordExistException : Exception
{
    private const string DefaultMessage = "The record exist";

    public KsRecordExistException(string? message = null)
        : base(
            string.IsNullOrEmpty(message)
            ? DefaultMessage
            : message)
    {
    }
}