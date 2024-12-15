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

    public Car? GetById(int id)
    {
        try
        {
            return this._carRepository.GetOneById(id);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }

    public Car? Update( Car value)
    {
        throw new NotImplementedException();
    }
    
    public bool Delete(Car value)
    {
        try
        {
            return this._carRepository.Delete(value);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }

    public Car? Create(Car value)
    {
        try
        {
            return this._carRepository.Create(value);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }

    public List<Parking> GetAllParking(bool onlyAvailable)
    {
        try
        {
            return this._carRepository.GetAllParkingCode(onlyAvailable);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }
    
    public Parking GetParkingCode(int parking_code)
    {
        try
        {
            return this._carRepository.GetParkingCode(parking_code);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }
    
}