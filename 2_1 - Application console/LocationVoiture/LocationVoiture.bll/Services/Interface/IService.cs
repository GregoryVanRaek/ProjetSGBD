namespace LocationVoiture.bll.Services;

public interface IService<Key, T>
{
    public List<T> GetAll();
    public T? GetById(Key key);
    public T? Update(Key key, T value);
    public T? Patch(Key key, T value);
    public bool Delete(Key key );
    public T? Create(T value);
}