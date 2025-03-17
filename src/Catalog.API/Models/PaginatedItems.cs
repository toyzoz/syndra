using System.Collections;
using System.ComponentModel;

namespace Catalog.API.Models;

public class PaginatedItems<T>(
    int pageSize,
    int pageIndex,
    long count,
    IEnumerable data)
{
    [property: Description("每页的条目数量。")] public int PageSize { get; } = pageSize;

    [property: Description("当前页码。")] public int PageIndex { get; } = pageIndex;

    [property: Description("条目总数。")] public long Count { get; } = count;

    [property: Description("当前页的条目列表。")] public IEnumerable Data { get; set; } = data;
}