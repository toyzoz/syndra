namespace Basket.API.Services;

public interface IUserIdentityService
{
    public string? GetUserIdentity();
    public string? GetUserName();
}
