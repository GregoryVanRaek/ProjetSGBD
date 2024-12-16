namespace LocationVoiture.dal.CustomException;

public class AlreadyExistException : Exception
{
    public AlreadyExistException(string entity)
        :base($"This {entity} already exist."){}
}