using LocationVoiture.bll.Services;
using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation
{
    public class RentalController
    {
        private readonly IRentalService _rentalService;
        private readonly CarController _carController;
        private readonly ClientController _clientController;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;

        public RentalController(ICarService carService, IRentalService rentalService, CarController carController, ClientController clientController, IClientService clientService)
        {
            this._rentalService = rentalService;
            this._carController = carController;
            this._clientController = clientController;
            this._carService = carService;
            this._clientService = clientService;
        }

        public void DisplayMenu()
        {
            this._rentalService.UpdateRentalState(); // mettre à jour le statut de la location si elle concerne la date du jour
            
            int choice = 0;
            
            while (choice != 5)
            {
                DisplayOptions();
                choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
                switch (choice)
                {
                    case 1 : GetAllRental(true);
                        break;
                    case 2 : CreateRental();
                        break;
                    case 3 : UpdateRental();
                        break;
                    case 4 : DeleteRental();
                        break;
                    case 5 : Console.WriteLine("Back to menu");
                        break;
                    default : Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
        
        #region Crud

        private void GetAllRental(bool withCompletedAndCancellled)
        {
            ConsoleAccess.CreateScreen("All Rentals");
            DisplayHeader();
            try
            { 
                List<Rental> rentals = _rentalService.GetAll(withCompletedAndCancellled);
                DisplayRental(rentals);
                ConsoleAccess.Wait();
            }
            catch (DBAccessException e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
            }
        }

        private void CreateRental()
        {
            Rental rental;
            int carId, clientId, duration;
            DateTime startDate;
            decimal amount;
            RentalStatus status;
            
            ConsoleAccess.CreateScreen("New Rental");
            
            try
            {
                // Voiture disponibles
                Console.WriteLine("Available cars : ");
                List<Car> cars = this._carService.GetAll(true);
                this._carController.DisplayAllCars(true);
                do
                {
                    carId = ValueControl.CheckPositiveInt("Enter the ID of an available car: ");
                    if (!cars.Any(car => car.Id == carId))
                        Console.WriteLine("Invalid Car ID. Please select a valid ID of the available cars.");
                    
                } while (!cars.Any(car => car.Id == carId));
                
                Console.Clear();
                
                // Client existant/nouveau
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Choose an existing client");
                Console.WriteLine("2. Create a new client");
        
                int choice = ValueControl.CheckPositiveInt("Your choice (1/2): ");
                
                if (choice == 1)
                {
                    Console.WriteLine("Available clients:");
                    this._clientController.GetAllClient(); 
                    clientId = ValueControl.CheckPositiveInt("Client ID : ");
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Creating a new client...");
                    clientId = this._clientController.CreateClient().Id; 
                    Console.WriteLine($"New client created with ID: {clientId}");
                }
                else
                {
                    throw new Exception("Invalid choice. Please select 1 or 2.");
                }
                
                // autres données
                do
                {
                    startDate = ValueControl.CheckDate("Start date : ");
                    if(startDate < DateTime.Now)
                        Console.WriteLine("Start date must be in the future.");
                } while (startDate < DateTime.Now);
                
                duration = ValueControl.CheckPositiveInt("Duration (in days) : ");
                
                if (_rentalService.CheckDuplicateRental(carId, startDate, duration))
                {
                    Console.WriteLine("This car is already rented or reserved for the selected dates.");
                    ConsoleAccess.Wait();
                    return;
                }

                amount = this._carService.CalculateRentalAmount(carId, duration);
                Console.WriteLine("Amount of rentals : " + amount);
                ConsoleAccess.Wait();
                status = startDate > DateTime.Today ? RentalStatus.reserved : RentalStatus.rent;
                
                // supprimer l'emplacement de parking si la voiture est en location actuellement
                if (status == RentalStatus.rent)
                    this._carService.UpdateCarParking(carId);
                
                rental = new Rental
                {
                    CarId = carId,
                    ClientId = clientId,
                    StartDate = startDate,
                    Duration = duration,
                    Amount = amount,
                    Status = status
                };
                
                Rental? createdRental = _rentalService.Create(rental);

                if (createdRental is not null)
                {
                    Console.WriteLine("Rental created successfully");
                    DisplayHeader();
                    DisplayRental(createdRental);
                }
                else
                    Console.WriteLine("An error occurred during the creation of the rental");
                
                ConsoleAccess.Wait();
            }
            catch (DBAccessException e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
            }
        }

        private Rental? UpdateRental()
        {
            ConsoleAccess.CreateScreen("Rental enclosing");
        
            if(this._carController.CheckParkingAvailability() == 0)
            {
                Console.WriteLine("You can't enclose this rental because there is no parking availability for the car.");
                ConsoleAccess.Wait();
                return null;
            }
            
            try
            {
                this.GetAllRental(false);
                
                int id = ValueControl.CheckPositiveInt("Enter rental's id : ");
            
                Rental? rental = _rentalService.GetById(id);

                if (rental is not null)
                {
                    int choice = 0;
                    bool done = false;
                    do
                    {
                        choice = ValueControl.CheckPositiveInt("Is this rental completed(1) or cancelled(2) ? ");
                        
                        if(choice == 1 && rental.Status == RentalStatus.reserved)
                            Console.WriteLine("You can't enclose this rental because rental status is reserved.");
                        else if (choice == 2 && rental.Status == RentalStatus.rent)
                            Console.WriteLine("You can't cancel a current rental ");
                        else if(choice == 1 || choice == 2)
                            done = true;
                    } while (!done);
                    
                    switch (choice)
                    {
                        case 1 : rental.Status = RentalStatus.completed; break;
                        case 2 : rental.Status = RentalStatus.cancelled; break;
                    }
                    DisplayHeader();
                    DisplayRental(rental);
                                        
                    ConsoleAccess.Wait();
                    
                    Rental? updatedRental = this._rentalService.Update(rental);
                    if (updatedRental is not null && rental.Status == RentalStatus.completed)
                    {
                        bool carUpdated = this._carService.GetFreeParkingPlace(rental.CarId);
                        if (carUpdated)
                        {
                            Console.WriteLine("Car status updated successfully with a free parking spot.");
                            ConsoleAccess.Wait();
                        }
                        else
                        {
                            Console.WriteLine("Car status update failed: No free parking spot found.");
                            ConsoleAccess.Wait();
                        }
                    }

                    return updatedRental;
                }
        
                Console.WriteLine("Rental not found");
                return null;
            }
            catch (DBAccessException e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ConsoleAccess.Wait();
                return null;
            }
        }

        private bool DeleteRental()
        {
            int id;
            
            ConsoleAccess.CreateScreen("Delete Rental");

            id = ValueControl.CheckPositiveInt("Enter rental's id : ");

            try
            {
                Rental? rental = _rentalService.GetById(id);

                if (rental is not null)
                {
                    DisplayHeader();
                    DisplayRental(rental);
                    Console.Write("Are you sure you want to delete this rental? (y/n) : ");
                    string choice = Console.ReadLine().ToString().ToLower();
                    switch (choice)
                    {
                        case "y" : _rentalService.Delete(rental);
                            Console.WriteLine("Rental deleted");
                            ConsoleAccess.Wait();
                            return true;
                        case "n": break;
                        default: Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else
                    Console.WriteLine("Rental not found");
                
            }
            catch (DBAccessException e)
            {
                Console.WriteLine(e.Message);
                ConsoleAccess.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ConsoleAccess.Wait();
            }

            return false;
        }
        
        #endregion

        #region private methods
        
        private void DisplayOptions()
        {
            ConsoleAccess.CreateScreen("Rental menu");
            Console.WriteLine("1. All Rentals");
            Console.WriteLine("2. Add new Rental");
            Console.WriteLine("3. Return/cancel Rental");
            Console.WriteLine("4. Delete Rental");
            Console.WriteLine("5. Back to main menu");
        }
        private void DisplayHeader()
        {
            Console.WriteLine("ID".PadRight(5) +
                              "Car ID".PadRight(10) +
                              "Client ID".PadRight(10) +
                              "Start Date".PadRight(15) +
                              "Duration".PadRight(10) +
                              "Amount".PadRight(15) +
                              "Status".PadRight(15));
            Console.WriteLine(new string('-', 80));
        }
        private void DisplayRental(Rental rental)
        {
            Console.WriteLine(rental.Id.ToString().PadRight(5) +
                              rental.CarId.ToString().PadRight(10) +
                              rental.ClientId.ToString().PadRight(10) +
                              rental.StartDate.ToString("yyyy-MM-dd").PadRight(15) +
                              rental.Duration.ToString().PadRight(10) +
                              rental.Amount.ToString("F").PadRight(15) +
                              rental.Status.ToString().PadRight(15));
        }
        private void DisplayRental(List<Rental> rentals)
        {
            foreach(Rental rental in rentals)
                DisplayRental(rental);
        }
        #endregion
    }
}
