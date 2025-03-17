using System.Security.Claims;
using Grpc.Core;

namespace Basket.API.Extensions;

public static class ServerCallContextExtensions
{
    public static string? GetUserIdentity(this ServerCallContext context) =>
        context.GetHttpContext().User.FindFirst("sub")?.Value;

    public static string? GetUserName(this ServerCallContext context) =>
        context.GetHttpContext().User.FindFirst(x => x.Type == ClaimTypes.Name)?.Value;
}
