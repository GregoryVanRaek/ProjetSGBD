using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;

namespace LocationVoiture.bll.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        this._clientRepository = clientRepository;
    }
    
    public List<Client> GetAll()
    {
        try
        {
            return this._clientRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }

    public Client? GetById(int id)
    {
        try
        {
            return this._clientRepository.GetOneById(id);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }
    
    public Client? GetOneByName(string name)
    {
        try
        {
            name = name.ToLower();
            return this._clientRepository.GetOneByName(name);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }
    
    public Client? Update(Client entity)
    {
        try
        {
            if (this._clientRepository.GetOneByEmail(entity.Email) is not null
                && this._clientRepository.GetOneByEmail(entity.Email).Id != entity.Id )
                throw new AlreadyExistException("user email");

            if (DateTime.Today.Year - entity.BirthDate.Year < 21)
                throw new Exception("The driver must be at least 21 years old.");

            return this._clientRepository.Update(entity);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }
    
    public bool Delete(Client entity)
    {
        try
        {
            return this._clientRepository.Delete(entity);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }

    public Client? Create(Client entity)
    {
        try
        {
            if (this._clientRepository.GetOneByEmail(entity.Email) is not null)
                throw new AlreadyExistException("user email");
            
            if(DateTime.Today.Year - entity.BirthDate.Year < 21 )
                throw new Exception("The driver must be at least 21 years old.");
            
            return this._clientRepository.Create(entity);
        }
        catch (Exception e)
        {
            throw new ServiceErrorException("Client", e);
        }
    }
}