namespace LocationVoiture.bll.Services;

public interface IService<Key, T>
{
    public List<T> GetAll();
    public T? GetById(Key key);
    public T? Update(T value);
    public bool Delete(T value);
    public T? Create(T value);
}