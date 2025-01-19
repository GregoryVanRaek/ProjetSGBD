namespace LocationVoiture.dal.Entities;

public class Address
{
    public required string Street { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }

    public override string ToString()
    {
        return $"Street: {Street}, PostalCode: {PostalCode}, City: {City}, Country: {Country}";
    }
}