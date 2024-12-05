namespace LocationVoiture.dal.Entities;

public class Model
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Brand { get; set; }
    public required int SeatNumber { get; set; }
    public required int CategoryId { get; set; }
}