namespace Ordering.Domain.AggregateModels.Buyers;

public interface IBuyerRepository
{
    Buyer Add(Buyer buyer);
    Buyer Update(Buyer buyer);
    Task<Buyer?> FindAsync(string identity);
    Task<Buyer?> FindByIdAsync(int id);
}