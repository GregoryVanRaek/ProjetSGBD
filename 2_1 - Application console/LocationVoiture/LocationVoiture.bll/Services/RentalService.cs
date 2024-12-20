using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;

namespace LocationVoiture.bll.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarService _carService;
        
        public RentalService(IRentalRepository rentalRepository, ICarService carService)
        {
            this._rentalRepository = rentalRepository;
            this._carService = carService;
        }
        
        public List<Rental> GetAll(bool withCompletedAndCancellled)
        {
            try
            {
                return this._rentalRepository.GetAll(withCompletedAndCancellled);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public List<Rental> GetAll()
        {
            try
            {
                return this.GetAll(true);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public Rental? GetById(int id)
        {
            try
            {
                return this._rentalRepository.GetOneById(id);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public Rental? Update(Rental value)
        {
            try
            {
                return this._rentalRepository.Update(value);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public bool Delete(Rental value)
        {
            try
            {
                return this._rentalRepository.Delete(value);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public Rental? Create(Rental value)
        {
            try
            {
                return this._rentalRepository.Create(value);
            }
            catch (Exception e)
            {
                throw new Exception("Rental service error : " + e.Message);
            }
        }
        
        public bool CheckDuplicateRental(int carId, DateTime startDate, int duration)
        {
            var rentals = this.GetAll().Where(r => r.CarId == carId);

            DateTime endDate = startDate.AddDays(duration);

            return rentals.Any(r => (r.Status == RentalStatus.rent || r.Status == RentalStatus.reserved) &&
                                    (startDate < r.StartDate.AddDays(r.Duration) &&
                                     endDate > r.StartDate));
        }
        
        public void UpdateRentalState()
        {
            List<Rental> rentals = this.GetAll(); 
    
            foreach (var rental in rentals)
            {
                if (rental.Status == RentalStatus.reserved && rental.StartDate <= DateTime.Now)
                {
                    rental.Status = RentalStatus.rent;
                    this.Update(rental); // mettre à jour le statut 
                    this._carService.UpdateCarParking(rental.CarId); // la voiture a quitté le parking
                }
            }
        }
        
    }
}
