namespace Sadin.Common.CustomExceptions;

public class KsDuplicatedUserException: Exception
{
    public KsDuplicatedUserException(string userNam)
        : base($"{userNam} is exist.")
    {
    }
}