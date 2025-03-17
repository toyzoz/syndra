namespace Ordering.Domain.AggregateModels.Buyers;

public class CardType
{
    private CardType()
    {
    }

    private CardType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;

    public static CardType CreateInstance(int id, string name)
    {
        return new CardType(id, name);
    }
}