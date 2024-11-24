namespace LocationVoiture.dal.Entities;

public class Car
{   
    public required int Id { get; set; }
    public required string LicensePlate { get; set; }
    public required string Color { get; set; }
    public required string ParkingCode { get; set; }
    public required string ModelName { get; set; }

    public override string ToString()
    {
        return $"{Id} {LicensePlate} {Color} {ParkingCode} {ModelName}";
    }
}   