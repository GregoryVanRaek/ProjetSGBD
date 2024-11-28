﻿using LocationVoiture.bll.Services;
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
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void CreateCategory()
    {
        Category category;
        string name;
        decimal dailyRate;
        ConsoleAccess.CreateScreen("New Category");
        
        try
        {
            name = ConsoleAccess.ReadInput<string>("Category's name : ");
            dailyRate = ConsoleAccess.ReadInput<decimal>("Daily rate : ");
            
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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
    } 

    private void UpdateCategory()
    {
        throw new NotImplementedException();
    }

    private void DeleteCategory()
    {
        throw new NotImplementedException();
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