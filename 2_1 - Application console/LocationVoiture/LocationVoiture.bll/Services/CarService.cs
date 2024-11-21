using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories;

namespace LocationVoiture.bll.Services;

public class CarService : ICarService
{
    private ICarRepository _carRepository; // injection de dépendance

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    public List<Car> GetAll()
    {
        try
        {
            return this._carRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }

    public Car? GetById(int key)
    {
        throw new NotImplementedException();
    }

    public Car? Update(int key, Car value)
    {
        throw new NotImplementedException();
    }

    public Car? Patch(int key, Car value)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int key)
    {
        throw new NotImplementedException();
    }

    public Car? Create(Car value)
    {
        throw new NotImplementedException();
    }
    
}