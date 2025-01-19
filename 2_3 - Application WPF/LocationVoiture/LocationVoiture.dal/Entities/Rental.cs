namespace LocationVoiture.dal.Entities;

public class Rental
{
    public int Id { get; set; }
    public required int CarId { get; set; }
    public required int ClientId { get; set; }
    public required DateTime StartDate { get; set; }
    public required int Duration { get; set; }
    public required decimal Amount { get; set; }
    public required RentalStatus Status { get; set; }
}

public enum RentalStatus
{
    reserved, rent, completed, cancelled
}