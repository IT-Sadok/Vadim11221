namespace PrivateHospitals.Core.Models;

public class FilteredResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }
}