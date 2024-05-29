namespace Sadin.Common.RequestOptions;

public class SearchRequestOptions : OrderingRequestOptions
{
    public string? SearchTerm { get; set; } = "";
}