using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories;

public interface ICarRepository : IRepository<int, Car>
{
    Parking GetParkingCode(int parking_code);
    List<Parking> GetAllParkingCode(bool onlyAvailable);
}