using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories.Interface;

namespace LocationVoiture.bll.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        
        public RentalService(IRentalRepository rentalRepository)
        {
            this._rentalRepository = rentalRepository;
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
        
        
    }
}
