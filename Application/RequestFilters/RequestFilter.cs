namespace Application.RequestFilters;
public record RequestFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;


}
