using LocationVoiture.bll.Services;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation;

public class CarController
{
    private readonly CategoryController _categoryController;
    private readonly ModelController _modelController;

    public CarController(CategoryController categoryController, ModelController modelController)
    {
        this._categoryController = categoryController;
        this._modelController = modelController;
    }

    public void CarMenu()
    {
        int choice = 0;
        
        while (choice != 9)
        {
            DisplayMainOptions();
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
            switch (choice)
            {
                case 1 : this._categoryController.DisplayMenu();
                    break;
                case 2 : this._modelController.DisplayMenu();
                    break;
                case 6 : Console.WriteLine("back to menu");
                    break;
                default : Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    
    

    #region Private methods
    
    private void DisplayMainOptions()
    {
        ConsoleAccess.CreateScreen("Car management menu");
        Console.WriteLine("1. Car Category");
        Console.WriteLine("2. Car Model");
        Console.WriteLine("9. Back to main menu");
    }
    
    #endregion
    
}