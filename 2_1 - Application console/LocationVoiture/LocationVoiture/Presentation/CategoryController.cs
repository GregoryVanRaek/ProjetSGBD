using LocationVoiture.bll.Services;
using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation;

public class CategoryController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }

    public void DisplayMenu()
    {
        int choice = 0;
        
        while (choice != 5)
        {
            DisplayOptions();
            choice = ValueControl.CheckPositiveInt("Enter your choice : ");
            switch (choice)
            {
                case 1 : GetAllCategory();
                    break;
                case 2 : CreateCategory();
                    break;
                case 3 : UpdateCategory();
                    break;
                case 4 : DeleteCategory();
                    break;
                case 5 : Console.WriteLine("back to menu");
                    break;
                default : Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    
    #region Crud

    private void GetAllCategory()
    {
        ConsoleAccess.CreateScreen("All Category");
        DisplayHeader();
        try
        {
            List<Category> categories = _categoryService.GetAll();
            DisplayCategory(categories);
            ConsoleAccess.Wait();
        }
        catch (DBAccessException e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
        catch (ServiceErrorException e)
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
    private void CreateCategory()
    {
        Category category;
        string name = "";
        decimal dailyRate;
        
        ConsoleAccess.CreateScreen("New Category");
        
        try
        {
            List<Category> existingCategories = _categoryService.GetAll();

            do
            {
                name = ValueControl.CheckString(name, "name");
                
                if(existingCategories.Select(x => x.Name.ToLower()).Contains(name))
                    Console.WriteLine("This category already exists");
                
            } while (existingCategories.Select(x => x.Name.ToLower()).Contains(name));
            
            dailyRate = ValueControl.CheckPositiveDecimal("Daily rate : ");
            
            category = new Category
            {
                Name = name,
                DailyRate = dailyRate
            };
            
            Category? createdCategory = _categoryService.Create(category);

            if (createdCategory is not null)
            {
                Console.WriteLine("Category created successfully");
                DisplayHeader();
                DisplayCategory(createdCategory);
            }
            else
                Console.WriteLine("An error occured during the creation of the category");
            
            ConsoleAccess.Wait();
        }
        catch (DBAccessException e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
        catch (ServiceErrorException e)
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
    private Category? UpdateCategory()
    {
        ConsoleAccess.CreateScreen("Update category");

        try
        {
            List<Category> existingCategories = _categoryService.GetAll();
            
            int id = ValueControl.CheckPositiveInt("Enter category's id : ");
            Category? category = _categoryService.GetById(id);

            if (category is not null)
            {
                DisplayHeader();
                DisplayCategory(category);

                do
                {
                    category.Name = ValueControl.CheckString(category.Name, "Category's name : ");
                
                    if(existingCategories.Select(x => x.Name.ToLower()).Contains(category.Name))
                        Console.WriteLine("This category already exists");
                
                } while (existingCategories.Select(x => x.Name.ToLower()).Contains(category.Name));

                category.DailyRate = ValueControl.CheckPositiveDecimal("Daily rate : ");

                DisplayCategory(category);
                ConsoleAccess.Wait();
                return this._categoryService.Update(category);
            }
            Console.WriteLine("Category not found");
            return null;
        }
        catch (DBAccessException e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
            return null;
        }
        catch (ServiceErrorException e)
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
    private bool DeleteCategory()
    {
        int id;
        
        ConsoleAccess.CreateScreen("Delete category");

        id = ValueControl.CheckPositiveInt("Enter category's id : ");

        try
        {
            Category? category = _categoryService.GetById(id);

            if (category is not null)
            {
                DisplayHeader();
                DisplayCategory(category);
                Console.Write("Are you sure you want to delete this category? (y/n) : ");
                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "y" : _categoryService.Delete(category);
                        Console.WriteLine("Category deleted");
                        ConsoleAccess.Wait();
                        return true;
                    case "n":
                        return false; 
                    default: Console.WriteLine("Invalid choice");ConsoleAccess.Wait();
                        break;
                }
            }
            else
                Console.WriteLine("Category not found");
        }
        catch (DBAccessException e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
        catch (ServiceErrorException e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }

        return false;
    }
    
    #endregion

    #region private methods
    
    private void DisplayOptions()
    {
        ConsoleAccess.CreateScreen("Category menu");
        Console.WriteLine("1. All Category");
        Console.WriteLine("2. Add new Category");
        Console.WriteLine("3. Update Category");
        Console.WriteLine("4. Delete Category");
        Console.WriteLine("5. Back to main menu");
    }
    private void DisplayHeader()
    {
        Console.WriteLine("ID".PadRight(5) +
                          "Name".PadRight(15) +
                          "Daily rate".PadRight(15));
        Console.WriteLine(new string('-', 50));
    }
    private void DisplayCategory(Category category)
    {
        Console.WriteLine(category.Id.ToString().PadRight(5) +
                          category.Name.PadRight(15) +
                          category.DailyRate.ToString("F").PadRight(15));
    }
    private void DisplayCategory(List<Category> categories)
    {
        foreach(Category category in categories)
            DisplayCategory(category);
    }
    
    #endregion
    
}