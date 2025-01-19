using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories.Interface;

public interface IClientRepository : IRepository<int, Client>
{
    public Client? GetOneByName(string name);
    public Client? GetOneByEmail(string email);
}