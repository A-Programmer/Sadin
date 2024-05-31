namespace Sadin.Application;

public class CqrsBase
{
    protected readonly IUnitOfWork UnitOfWork;

    public CqrsBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
}