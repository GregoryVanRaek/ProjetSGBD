namespace LocationVoiture.dal.CustomException;

public class DBAccessException : Exception
{
    public string Details { get; private set; }

    public DBAccessException(string origin, string details) : base(origin)
    {
        this.Details = details;
    }
    
}