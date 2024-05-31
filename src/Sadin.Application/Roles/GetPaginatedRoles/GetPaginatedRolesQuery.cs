using System.Linq.Expressions;
using Sadin.Common.RequestOptions;
using Sadin.Common.Utilities;
using Sadin.Domain.Aggregates.Roles;

namespace Sadin.Application.Roles.GetPaginatedRoles;

public class GetPaginatedRolesQuery
    : IQuery<PaginatedList<RolesListItemResponse>>
{
    public GetPaginatedRolesQuery(SearchRequestOptions options)
    {
        if(options.SearchTerm.HasValue())
        {
            Where = x =>
                x.Name.ToLower().Contains(options.SearchTerm.ToLower()) ||
                x.Description.ToLower().Contains(options.SearchTerm.ToLower());
        }
        OrderByPropertyName = options.OrderByPropertyName.HasValue() ?  options.OrderByPropertyName : "Id";
        Desc = options.Desc;
        PageIndex = options.PageIndex;
        PageSize = options.PageSize;
    }
    
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<Role, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}