using System.Text.Json.Serialization;

namespace Sadin.Presentation;

public class ResultMessage
{
    public bool IsSuccess { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    protected ResultMessage()
    {

    }

    public ResultMessage(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}

public class ResultMessage<TData> : ResultMessage
{
    public ResultMessage()
    {

    }
    public ResultMessage(bool isSuccess, TData data, string message) : base(isSuccess, message)
    {
        Data = data;

    }

    public ResultMessage(bool isSuccess,TData data,  string? message, int? pageIndex, int? totalPages, int? totalItems,
        bool? showPagination) : base(isSuccess, message)
    {
        Data = data;
        PageIndex = pageIndex;
        TotalPages = totalPages;
        TotalItems = totalItems;
        ShowPagination = showPagination;

    }


    public TData Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? PageIndex { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalPages { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TotalItems { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowPagination { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasPreviousPage
    {
        get
        {
            if (ShowPagination == null || ShowPagination == false)
                return null;

            PageIndex = PageIndex ?? 0;
            return (PageIndex > 1);
        }
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? HasNextPage
    {
        get
        {
            if (ShowPagination == null || ShowPagination == false)
                return null;

            PageIndex = PageIndex ?? 0;
            TotalPages = TotalPages ?? 0;
            return (PageIndex < TotalPages);
        }
    }
}
