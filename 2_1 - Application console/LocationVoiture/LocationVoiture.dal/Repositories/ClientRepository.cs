using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly DBAccess _connection;

    public ClientRepository()
    {
        _connection = new DBAccess(); 
    }
    
    public List<Client> GetAll()
    {
        throw new NotImplementedException();
    }

    public Client? GetOneById(int key)
    {
        throw new NotImplementedException();
    }

    public Client Create(Client entity)
    {
        throw new NotImplementedException();
    }

    public Client Update(Client entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Client entity)
    {
        throw new NotImplementedException();
    }
}