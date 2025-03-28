﻿namespace Ordering.Domain.AggregateModels.Orders;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetListAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<Order> AddAsync(Order order);
}