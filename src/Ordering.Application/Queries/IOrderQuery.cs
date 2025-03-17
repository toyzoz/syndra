using Ordering.Application.Queries.ViewModels;
using Ordering.Domain.AggregateModels.Orders;

namespace Ordering.Application.Queries;

public interface IOrderQuery
{
    Task<List<CardTypeOutput>> GetCardTypesAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetOrderByUserAsync(string user);
}