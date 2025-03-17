namespace Ordering.Domain.AggregateModels.Buyers;

public class CardType
{
    public int Id { get;private set; }
    public string Name { get; private set; } = null!;

    private CardType()
    {
    }

    private CardType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static CardType CreateInstance(int id, string name)
    {
        return new CardType(id, name);
    }
}