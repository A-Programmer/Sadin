namespace Sadin.Common.CustomExceptions;

public sealed class KsRecordExistException : KsException
{
    public KsRecordExistException(string? message = null)
        : base(message ?? "Same record is exist.")
    {
    }
}