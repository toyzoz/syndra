using System.ComponentModel;

namespace Catalog.API.Models;

public class PaginatedItems<T>(
    int pageSize,
    int pageIndex,
    long count,
    List<T> data)
{
    [property: Description("每页的条目数量。")] public int PageSize { get; set; } = pageSize;

    [property: Description("当前页码。")] public int PageIndex { get; set; } = pageIndex;

    [property: Description("条目总数。")] public long Count { get; set; } = count;

    [property: Description("当前页的条目列表。")] public List<T> Data { get; set; } = data;
}
