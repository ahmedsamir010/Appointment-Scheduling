namespace Application.RequestFilters;
public record RequestFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchValue { get; set; }
    public string? SortColumn { get; set; }
    public string? SortDirection { get; set; } = "ASC";
}