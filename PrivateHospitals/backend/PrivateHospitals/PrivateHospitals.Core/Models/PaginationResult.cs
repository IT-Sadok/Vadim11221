namespace PrivateHospitals.Core.Models;

public class PaginationResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    
    public PaginationResult(IEnumerable<T> items, int totalItems, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        Items = items.ToList();
    }
}