using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;
using Catalog.API.Models;
using Xunit;

namespace Catalog.API.FunctionalTests;

public class ItemControllerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetListAsync_ReturnsOkResult()
    {
        const int pageSize = 20;
        const int pageIndex = 0;
        // Act
        var response = await Client.GetAsync($"/Items?PageSize={pageSize}&PageIndex={pageIndex}");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<PaginatedItems<CatalogItem>>();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(101, result.Count);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(pageIndex, result.PageIndex);
    }

    [Fact]
    public async Task GetByIdsAsync_ReturnsOkResult()
    {
        int[] ids = [1, 2, 3];
        var queryString = HttpUtility.ParseQueryString(string.Empty);

        foreach (var id in ids) queryString.Add("ids", id.ToString());

        // Act
        var response = await Client.GetAsync($"/Items?{queryString}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<List<CatalogItem>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public async Task CreateAsync_ReturnsCreatedAtRoute()
    {
        // Arrange
        CatalogItem newItem = new()
        {
            Name = "Test Item",
            Description = "Test Description",
            Price = 10.0m,
            PictureFileName = "test.png",
            CatalogBrandId = 1,
            CatalogTypeId = 1,
            AvailableStock = 100,
            RestockThreshold = 10,
            MaxStockThreshold = 1000
        };
        // Act
        var create = await Client.PostAsJsonAsync("/items", newItem);
        create.EnsureSuccessStatusCode();

        var get = await Client.GetAsync(create.Headers.Location);
        get.EnsureSuccessStatusCode();
      
        // Assert
        Assert.Equal(HttpStatusCode.Created, create.StatusCode);
        Assert.NotNull(create.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, get.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNoContent_ForValidId()
    {
        // Arrange
        // Act
        var responseDelete = await Client.DeleteAsync("items/1");
        responseDelete.EnsureSuccessStatusCode();

        var responseGet = await Client.GetAsync("/items/1");

        Assert.Equal(HttpStatusCode.NoContent, responseDelete.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotFound_ForInvalidId()
    {
        // Arrange
        // Act
        var responseDelete = await Client.DeleteAsync("items/9999");
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, responseDelete.StatusCode);
    }
}