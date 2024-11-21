using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories;

public interface IRepository<Key,T>
{
    public List<Car> GetAll();
    public T? GetOneById(Key key);
    public T Create(T entity);
    public T Update(T entity);
    public bool Delete(T entity);
}