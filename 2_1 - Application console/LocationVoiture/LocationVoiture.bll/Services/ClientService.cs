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
        throw new NotImplementedException();
    }

    public Client? GetById(int key)
    {
        throw new NotImplementedException();
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