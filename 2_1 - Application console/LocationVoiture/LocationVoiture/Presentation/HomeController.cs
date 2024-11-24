using Microsoft.Extensions.DependencyInjection;

namespace LocationVoiture.Presentation;

public class HomeController
{
    private readonly IServiceProvider _serviceProvider;
    
    public HomeController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public void MainMenu(IServiceProvider serviceProvider)
    {
        int choice = 0;

        while (choice != 4)
        {
            DefaultDisplay();
            choice = ConsoleAccess.ReadInput<int>("Your choice : ");
            try
            {
                switch (choice)
                {
                    case 1: 
                        // Résolution via DI
                        var currentMenu = _serviceProvider.GetRequiredService<ClientController>();
                        currentMenu.ClientMenu();
                        break;
                    case 2: 
                        Console.WriteLine("Choice 2");
                        break;
                    case 3: 
                        Console.WriteLine("Choice 3");
                        break;
                    case 4: 
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default: 
                        Console.WriteLine("This option doesn't exist");
                        ConsoleAccess.Wait();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }
    }

    public void DefaultDisplay()
    {
        ConsoleAccess.CreateScreen("Welcome to your rental management system");
        
        Console.WriteLine("1. Client");
        Console.WriteLine("2. Rentals");
        Console.WriteLine("3. Car management");
        Console.WriteLine("4. Exit");
    }
    
}