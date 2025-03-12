using Catalog.API.Controllers;
using Catalog.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Catalog.API.FunctionalTests
{
    public class BrandsControllerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task GetListAsync()
        {
            var response = await Client.GetAsync("/Brands");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}