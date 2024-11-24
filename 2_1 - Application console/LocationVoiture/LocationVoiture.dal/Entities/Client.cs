namespace LocationVoiture.dal.Entities;

public class Client
{
    public required int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required string DrivingLicense { get; set; }
    public required DateTime BirthDate { get; set; }
}