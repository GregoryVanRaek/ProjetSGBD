using LocationVoiture.dal.Entities;

namespace LocationVoiture.bll.Services;

public interface ICarService : IService<int, Car>
{
    List<Parking> GetAllParking(bool onlyAvailable);
    Parking GetParkingCode(int parking_code);
    List<Car> GetAll(bool onlyAvailable);
    Decimal GetAmount(int car_id);
    bool GetFreeParkingPlace(int carId);
    decimal CalculateRentalAmount(int carId, int duration);
    void UpdateCarParking(int id);
}