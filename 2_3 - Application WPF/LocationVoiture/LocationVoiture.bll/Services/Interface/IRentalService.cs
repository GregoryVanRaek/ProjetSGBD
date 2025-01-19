using LocationVoiture.dal.Entities;

namespace LocationVoiture.bll.Services;

public interface IRentalService : IService<int, Rental>
{
    List<Rental> GetAll(bool withCompletedAndCancellled);
    bool CheckDuplicateRental(int carId, DateTime startDate, int duration);
    void UpdateRentalState();
}