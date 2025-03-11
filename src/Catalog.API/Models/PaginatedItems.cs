namespace Catalog.API.Models
{
    public class PaginatedItems<T>(
        int pageSize,
        int pageIndex,
        long count,
        List<T> data)
    {
        public int PageSize { get; set; } = pageSize;
        public int PageIndex { get; set; } = pageIndex;
        public long Count { get; set; } = count;
        public List<T> Data { get; set; } = data;
    }
}