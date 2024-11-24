using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories;

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
            throw new Exception("Client service error : " + e.Message);
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
            throw new Exception("Client service error : " + e.Message);
        }
    }
    
    public Client? GetOneByName(string name)
    {
        try
        {
            return this._clientRepository.GetOneByName(name);
        }
        catch (Exception e)
        {
            throw new Exception("Client service error : " + e.Message);
        }
    }

    public Client? Update(int key, Client value)
    {
        throw new NotImplementedException();
    }

    public Client? Patch(int key, Client value)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int key)
    {
        throw new NotImplementedException();
    }

    public Client? Create(Client value)
    {
        throw new NotImplementedException();
    }
}