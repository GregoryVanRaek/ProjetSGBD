namespace LocationVoiture.dal.Entities;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal DailyRate { get; set; }
}