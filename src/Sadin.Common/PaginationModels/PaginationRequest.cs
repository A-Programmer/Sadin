namespace Sadin.Common.PaginationModels;

public class PaginationRequest
{
    public int? PageNumber { get; set; } = 1;
    public int? PageSize { get; set; } = 50;
    public string? SearchTerm { get; set; } = "";
    public string? OrderByPropertyName { get; set; } = "Id";
    public bool Desc { get; set; } = false;
}