namespace Sadin.Common.RequestOptions;

public class OrderingRequestOptions
    : PaginationRequestOptions
{
    public string OrderByPropertyName { get; set; } = "Id";
    public bool Desc { get; set; } = false;
}