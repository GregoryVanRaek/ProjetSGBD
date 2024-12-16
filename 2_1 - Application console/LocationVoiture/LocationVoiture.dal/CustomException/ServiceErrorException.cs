namespace LocationVoiture.dal.CustomException;

public class ServiceErrorException : Exception
{
    public ServiceErrorException(string serviceName, Exception e)
        : base($"{serviceName} service error : {e.Message}"){}
}