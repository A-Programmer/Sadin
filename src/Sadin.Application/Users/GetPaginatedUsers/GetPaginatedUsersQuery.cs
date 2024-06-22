using System.Linq.Expressions;
using Sadin.Common.RequestOptions;
using Sadin.Common.Utilities;

namespace Sadin.Application.Users.GetPaginatedUsers;

public class GetPaginatedUsersQuery
    : IQuery<PaginatedList<UsersListItemResponse>>
{
    public GetPaginatedUsersQuery(SearchRequestOptions options)
    {
        if(options.SearchTerm.HasValue())
        {
            Where = x =>
                x.Id.ToString().ToLower() == options.SearchTerm.ToLower() ||
                x.UserName.ToLower().Contains(options.SearchTerm.ToLower()) ||
                x.Email.ToLower().Contains(options.SearchTerm.ToLower()) ||
                x.Roles.Any(r => r.Name.ToLower() == options.SearchTerm.ToLower()) ||
                x.PhoneNumber.ToLower().Contains(options.SearchTerm.ToLower());
        }
        OrderByPropertyName = options.OrderByPropertyName.HasValue() ?  options.OrderByPropertyName : "Id";
        Desc = options.Desc;
        PageIndex = options.PageIndex;
        PageSize = options.PageSize;
    }
    
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public Expression<Func<User, bool>>? Where { get; private set; }
    public string OrderByPropertyName { get; private set; }
    public bool Desc { get; private set; }
}