using LocationVoiture.dal.Entities;

namespace LocationVoiture.bll.Services;

public interface ICarService : IService<int, Car>
{
    List<Parking> GetAllParking(bool onlyAvailable);
    Parking GetParkingCode(int parking_code);
}