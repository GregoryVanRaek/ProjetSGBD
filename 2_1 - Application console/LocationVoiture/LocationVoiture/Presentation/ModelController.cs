using LocationVoiture.bll.Services;
using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation;

public class ModelController
{
    private readonly IModelService _modelService;
    private readonly ICategoryService _categoryService;
    
    public ModelController(IModelService modelService, ICategoryService categoryService)
    {
        this._modelService = modelService;
        this._categoryService = categoryService;
    }
    
    public void DisplayMenu()
    {
        int choice = 0;
        
        while (choice != 5)
        {
            DisplayOptions();
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
            switch (choice)
            {
                case 1 : GetallModels();
                    break;
                case 2 : CreateModel();
                    break;
                case 3 : UpdateModel();
                    break;
                case 4 : DeleteModel();
                    break;
                case 5 : Console.WriteLine("back to menu");
                    break;
                default : Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    
    #region Crud

    private void GetallModels()
    {
        ConsoleAccess.CreateScreen("All models");
        DisplayHeader();
        try
        { 
            List<Model> models = _modelService.GetAll();
            DisplayModel(models);
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

    private void CreateModel()
    {
        Model model;
        string name = "", brand = "";
        int seatNumber = 0;
        Category? category ;
        
        ConsoleAccess.CreateScreen("New model");
        
        try
        {
            name = ValueControl.CheckString(name, "name : ");
            brand = ValueControl.CheckString(brand, "brand : ");

            do
            {
                seatNumber = ValueControl.CheckPositiveInt("Seat number : ");
                if(seatNumber <= 1)
                    Console.WriteLine("A model must have at least two seat");
            } while (seatNumber <= 1);
            
            category = _categoryService.GetById(this.CheckCategoryId());
            
            model = new Model
            {
                Name = name,
                Brand = brand,
                SeatNumber = seatNumber,
                CategoryId = category.Id,
                CategoryName = category.Name,
                DailyRate = category.DailyRate,
            };
            
            Model? createdModel = _modelService.Create(model);

            if (createdModel is not null)
            {
                Console.WriteLine("Model created successfully");
                DisplayHeader();
                DisplayModel(createdModel);
            }
            else
                Console.WriteLine("An error occured during the creation of the model");
            
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

    private Model? UpdateModel()
    {
        ConsoleAccess.CreateScreen("Update model");

        try
        {
            int id = ValueControl.CheckPositiveInt("Enter model's id : ");
        
            Model? model = _modelService.GetById(id);

            if (model is not null)
            {
                DisplayHeader();
                DisplayModel(model);

                model.Name = ValueControl.CheckString(model.Name, "Model's name : ");
                model.Brand = ValueControl.CheckString(model.Brand, "Model's brand : ");
                model.SeatNumber = ValueControl.CheckPositiveInt("Seat number : ");
                model.CategoryId = this.CheckCategoryId();

                DisplayModel(model);
                return this._modelService.Update(model);
            }
            else
            {
                Console.WriteLine("Model not found");
                return null;
            }
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
            return null;
        }
    }

    private bool DeleteModel()
    {
        int id;
        
        ConsoleAccess.CreateScreen("Delete model");

        id = ValueControl.CheckPositiveInt("Enter category's id : ");

        try
        {
            Model? model = _modelService.GetById(id);

            if (model is not null)
            {
                DisplayHeader();
                DisplayModel(model);
                Console.Write("Are you sure you want to delete this model? (y/n) : ");
                string choice = Console.ReadLine().ToString().ToLower();
                switch (choice)
                {
                    case "y" : _modelService.Delete(model);
                        Console.WriteLine("Model deleted");
                        ConsoleAccess.Wait();
                        return true;
                    case "n": break;
                    default: Console.WriteLine("Invalid choice");ConsoleAccess.Wait();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Model not found");
                ConsoleAccess.Wait();
            }
            
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
        ConsoleAccess.CreateScreen("Model menu");
        Console.WriteLine("1. All Models");
        Console.WriteLine("2. Add new model");
        Console.WriteLine("3. Update model");
        Console.WriteLine("4. Delete model");
        Console.WriteLine("5. Back to main menu");
    }
    
    public void DisplayHeader()
    {
        Console.WriteLine("ID".PadRight(5) +
                          "Name".PadRight(20) +
                          "Brand".PadRight(15) +
                          "Seat Number".PadRight(17) +
                          "Category".PadRight(20) +
                          "Price/Day".PadRight(10));
        Console.WriteLine(new string('-', 85));
    }
        
    public void DisplayModel(Model model)
    {
        Console.WriteLine(model.Id.ToString().PadRight(5) +
                          model.Name.PadRight(20) +
                          model.Brand.PadRight(20) +
                          model.SeatNumber.ToString().PadRight(12) +
                          (model.CategoryName ?? "N/A").PadRight(20) +
                          (model.DailyRate?.ToString() ?? "N/A").PadRight(10));
    }

    public void DisplayModel(List<Model> models)
    {
        foreach(Model model in models)
            DisplayModel(model);
    }

    private int CheckCategoryId()
    {
        List<Category> categories = this._categoryService.GetAll();
        int nbCategory = categories.Count;
        int value = 0;

        Console.WriteLine("Here is all the categories :");
        foreach(var c in categories)
            Console.WriteLine(c.Id + " - " + c.Name);
        
        do
        {
            value = ValueControl.CheckPositiveInt("Enter a valid category id: ");
            if (value > nbCategory)
                Console.WriteLine($"Category id must be between 1 and {nbCategory}");
        } while (value > nbCategory);

        return value;
    }
    
    #endregion
    
}