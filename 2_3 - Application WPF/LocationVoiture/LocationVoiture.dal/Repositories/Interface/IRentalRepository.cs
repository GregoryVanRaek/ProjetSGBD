using LocationVoiture.dal.Entities;

namespace LocationVoiture.dal.Repositories.Interface;

public interface IRentalRepository :IRepository<int, Rental>
{
    List<Rental> GetAll(bool withCompletedAndCancellled);
}