namespace LocationVoiture.dal.Entities;

public class Client
{
    public int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required Address Address { get; set; }
    public required string DrivingLicense { get; set; }
    public required DateTime BirthDate { get; set; }
    
    public Client(){}

    public override string ToString()
    {
        return Id.ToString().PadRight(5) +
               Lastname.PadRight(15) +
               Firstname.PadRight(15) +
               Email.PadRight(30) +
               Address.Street.PadRight(20) +
               Address.PostalCode.PadRight(12) +
               Address.City.PadRight(15) +
               Address.Country.PadRight(15) +
               DrivingLicense.PadRight(15) +
               BirthDate.ToString("d");
    }
}