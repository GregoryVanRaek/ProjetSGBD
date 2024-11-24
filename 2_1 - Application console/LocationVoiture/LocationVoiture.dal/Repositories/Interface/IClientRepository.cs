using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories;

public interface IClientRepository : IRepository<int, Client>
{
    public Client? GetOneByName(string name);
}