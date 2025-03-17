using Basket.API.Extensions;
using Grpc.Core;

namespace Basket.API.Services;

public class UserIdentityService(ServerCallContext context) : IUserIdentityService
{
    public string? GetUserIdentity() => context.GetUserIdentity();

    public string? GetUserName() => context.GetUserName();
}
