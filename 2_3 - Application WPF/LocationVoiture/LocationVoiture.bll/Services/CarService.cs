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
    
    public List<Car> GetAll(bool onlyAvailable)
    {
        try
        {
            return this._carRepository.GetAll(onlyAvailable);
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

    public Car? Update(Car value)
    {
        try
        {
            return this._carRepository.Update(value);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
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

    public Decimal GetAmount(int id)
    {
        try
        {
            return this._carRepository.GetAmount(id);
        }
        catch (Exception e)
        {
            throw new Exception("Error in car service : " + e.Message);
        }
    }
    
    public bool GetFreeParkingPlace(int carId)
    {
        try
        {
            return this._carRepository.GetFreeParkingPlace(carId);
        }
        catch (Exception e)
        {
            throw new Exception("Car service error: " + e.Message);
        }
    }

    public decimal CalculateRentalAmount(int carId, int duration)
    {
        return this.GetAmount(carId) * duration;
    }
    
    public void UpdateCarParking(int id)
    {
        Car car = this.GetById(id);

       bool isParkingAssigned = this.GetFreeParkingPlace(car.Id);

        if (isParkingAssigned)
            this.Update(car);
        else
            throw new Exception("No free parking space available.");
    }
}