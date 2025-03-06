using Ordering.Doamin.Orders;
using Ordering.Doamin.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Doamin.Events
{
    public record OrderCreatedDomainEvent(Order newOrder) : IDomainEvent;
}
