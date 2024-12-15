using LocationVoiture.bll.Services;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation;

public class CarController
{
    private readonly CategoryController _categoryController;
    private readonly ModelController _modelController;
    private readonly IModelService _modelService;
    private readonly ICarService _carService;

    public CarController(CategoryController categoryController, ModelController modelController, ICarService carService, IModelService modelService)
    {
        this._categoryController = categoryController;
        this._modelController = modelController;
        this._carService = carService;
        this._modelService = modelService;
    }

    public void CarMenu()
    {
        int choice = 0;
        
        while (choice != 8)
        {
            DisplayMainOptions();
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
            switch (choice)
            {
                case 1 : this.GetAllCars();
                    break;
                case 2 : this.GetAllAvailableCars();
                    break;
                case 3 : this.GetAllRentCars();
                    break;
                case 4 : this.CreateCar();
                    break;
                case 5 : this.DeleteCar();
                    break;
                case 6 : this._categoryController.DisplayMenu();
                    break;
                case 7 : this._modelController.DisplayMenu();
                    break;
                case 8 : Console.WriteLine("back to menu");
                    break;
                default : Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    private void GetAllCars()
    {
        ConsoleAccess.CreateScreen("All cars");
        DisplayHeader();
        try
        { 
            List<Car> cars = _carService.GetAll();
            DisplayCars(cars);
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private void CreateCar()
    {
        Car car;
        string license = "", color = "";
        Parking parking;
        Model? model;
        
        ConsoleAccess.CreateScreen("New Car");
    
        try
        {
            license = ValueControl.CheckString(license, "License plate : ");
            color = ValueControl.CheckString(color, "Color : ");
            parking = this._carService.GetParkingCode(this.CheckParkingAvailability());
            model = this._modelService.GetById(this.CheckModel());
            
            car = new Car
            {
                LicensePlate = license,
                Color = color,
                ParkingId = parking.Id,
                ModelId = model.Id,
            };

            Car? createdCar = _carService.Create(car);

            if (createdCar is not null)
            {
                Console.WriteLine("Car created successfully");
                DisplayHeader();
                DisplayCar(createdCar);
            }
            else
                Console.WriteLine("An error occured during the creation of the car");
        
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
    }
    private bool DeleteCar()
    {
        int id;
        
        try
        {
            ConsoleAccess.CreateScreen("Delete a car");
            this.DisplayAllCars(false);

            id = ValueControl.CheckPositiveInt("Enter the id of the car that you want to delete : ");
            
            Car? car = _carService.GetById(id);

            if (car is not null)
            {
                ConsoleAccess.CreateScreen("Delete a car");
                DisplayHeader();
                DisplayCar(car);
                Console.WriteLine("Are you sure you want to delete this car? (y/n) : ");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "y" : _carService.Delete(car);
                        Console.WriteLine("Car deleted");
                        ConsoleAccess.Wait();
                        return true;
                    case "n": break;
                    default: Console.WriteLine("Invalid choice");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Car not found");
                ConsoleAccess.Wait();
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ConsoleAccess.Wait();
        }

        return false;
    }
    private void GetAllAvailableCars()
    {
        ConsoleAccess.CreateScreen("All available cars");
        DisplayHeader();
        try
        { 
            List<Car> cars = _carService.GetAll(true);
            DisplayCars(cars);
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void GetAllRentCars()
    {
        ConsoleAccess.CreateScreen("All rent cars");
        DisplayHeader();
        try
        { 
            List<Car> availableCars = _carService.GetAll(true);
            List<Car> allCars = _carService.GetAll(false);
            
            List<Car> rentedCars = allCars.Where(c => !availableCars.Any(ac => ac.Id == c.Id)).ToList();
            
            DisplayCars(rentedCars);
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    #region Private methods
    
    private void DisplayMainOptions()
    {
        ConsoleAccess.CreateScreen("Car management menu");
        Console.WriteLine("1. All cars");
        Console.WriteLine("2. Available cars");
        Console.WriteLine("3. Rent cars");
        Console.WriteLine("4. Create new car");
        Console.WriteLine("5. Delete a car");
        Console.WriteLine("6. Categories");
        Console.WriteLine("7. Models");
        Console.WriteLine("8. Back to main menu");
    }
    
    private void DisplayHeader()
    {
        Console.WriteLine("ID".PadRight(5) +
                          "License Plate".PadRight(15) +
                          "Color".PadRight(10) +
                          "Parking Code".PadRight(15) +
                          "Model Name".PadRight(20));
        Console.WriteLine(new string('-', 65));
    }
        
    private void DisplayCar(Car car)
    {
        Console.WriteLine(car.Id.ToString().PadRight(5) +
                          car.LicensePlate.PadRight(15) +
                          car.Color.PadRight(15) +
                          car.ParkingCode.PadRight(10) +
                          car.ModelName.PadRight(20));
    }

    private void DisplayCars(List<Car> cars)
    {
        foreach (Car car in cars)
        {
            DisplayCar(car);
        }
    }

    public void DisplayAllCars(bool onlyAvailable)
    {
        DisplayHeader();
        if(onlyAvailable)
            DisplayCars(_carService.GetAll(onlyAvailable));
        else
            DisplayCars(_carService.GetAll());
    }
    
    private int CheckParkingAvailability()
    {
        List<Parking> available = new List<Parking>();
        int chosenPlace = 0;
        
        try
        {
            available = this._carService.GetAllParking(true);
            Console.WriteLine("Here are every available parking place :");
            do
            {
                DisplayParking(available);
                chosenPlace = ValueControl.CheckPositiveInt("Enter the id of the parking place : ");
                
                if(available.FirstOrDefault(c => c.Id == chosenPlace) is null)
                    Console.WriteLine("Please choose an available parking place");
                
            } while (available.FirstOrDefault(c => c.Id == chosenPlace) is null);
        }
        catch (Exception e)
        {
            throw new Exception("There was an error checking the parking place " + e.Message);
        }

        return chosenPlace;
    }

    private void DisplayParking(List<Parking> parkings)
    {
        Console.WriteLine("ID".PadRight(5) +
                          "Parking Code".PadRight(15));
        Console.WriteLine(new string('-', 30));

        foreach (Parking p in parkings)
        {
            Console.WriteLine(p.Id.ToString().PadRight(5) +
                              p.Code.PadRight(15));
        }
    }
    
    private int CheckModel()
    {
        List<Model> models = new List<Model>();
        int chosenModel = 0;
        
        try
        {
            models = this._modelService.GetAll();
            Console.WriteLine("Here is all models :");
            do
            {
                _modelController.DisplayHeader();
                _modelController.DisplayModel(models);
                
                chosenModel = ValueControl.CheckPositiveInt("Enter the id of the model : ");
                
                if(models.FirstOrDefault(c => c.Id == chosenModel) is null)
                    Console.WriteLine("Please choose a model specified above");
                
            } while (models.FirstOrDefault(c => c.Id == chosenModel) is null);
        }
        catch (Exception e)
        {
            throw new Exception("There was an error checking the model " + e.Message);
        }

        return chosenModel;
    }
    
    #endregion
    
}