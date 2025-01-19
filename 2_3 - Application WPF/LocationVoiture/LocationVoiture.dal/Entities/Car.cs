namespace LocationVoiture.dal.Entities;

public class Car
{   
    public int Id { get; set; }
    public required string LicensePlate { get; set; }
    public required string Color { get; set; }
    public int? ParkingId { get; set; }
    public string? ParkingCode { get; set; }
    public int? ModelId { get; set; }
    public string? ModelName { get; set; }

    public override string ToString()
    {
        return $"{Id} {LicensePlate} {Color} {ParkingCode} {ModelName}";
    }
}   