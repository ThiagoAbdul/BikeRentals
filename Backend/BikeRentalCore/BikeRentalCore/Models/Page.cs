namespace BikeRentalCore.Models;

public class Page<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int PageIndex { get; set; }
    public int PageSize {  get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }

    public Page(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
    {
        Items = items;
        PageIndex = pageIndex;
        PageSize = pageSize;
        double ratio = ((float)totalCount / (float)pageSize);
        double ceil = Math.Ceiling(ratio);
        PageCount = (int)ceil;
        TotalCount = totalCount;
    }

    public Page<TResult> Map<TResult>(Func<T, TResult> selector)
    {
        IEnumerable<TResult> items = Items.Select(selector).ToList();
        return new Page<TResult>(items, PageIndex, PageSize, TotalCount);  
    }
}
