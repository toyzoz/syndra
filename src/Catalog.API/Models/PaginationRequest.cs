using System.ComponentModel;

namespace Catalog.API.Models;

public record PaginationRequest(
    [property: Description("The number of items per page.")]
    [property: DefaultValue(10)]
    int PageSize = 10,
    [property: Description("The number of items per page.")]
    [property: DefaultValue(0)]
    int PageIndex = 0);