namespace Ordering.Domain.AggregateModels.Orders;

public record Address
{
    private Address(string Street, string City, string State, string Country, string ZipCode)
    {
        this.Street = Street;
        this.City = City;
        this.State = State;
        this.Country = Country;
        this.ZipCode = ZipCode;
    }

    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }

    public static Address Create(string street, string city, string state, string country, string zipCode)
    {
        return new Address(street, city, state, country, zipCode);
    }
}