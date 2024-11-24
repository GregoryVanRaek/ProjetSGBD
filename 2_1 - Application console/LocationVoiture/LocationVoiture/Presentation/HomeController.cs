namespace LocationVoiture.Presentation;

public class HomeController
{
    public void MainMenu()
    {
        int choice = 0;
        
        while (choice != 4)
        {
            DefaultDisplay();
            choice = ConsoleAccess.ReadInput<int>("You choice : ");
            try
            {
                switch (choice)
                {
                    case 1 : Console.WriteLine("choix 1");
                        break;
                    case 2 : Console.WriteLine("choix 2");
                        break;
                    case 3 : Console.WriteLine("choix 3");
                        break;
                    case 4 : Console.WriteLine("choix 4");
                        System.Environment.Exit(0);
                        break;
                    default : Console.WriteLine("This option doesn't exist");ConsoleAccess.Wait();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
    }

    public void DefaultDisplay()
    {
        ConsoleAccess.CreateScreen(
            "************************************************\n" + 
                   "*   Welcome to your rental management system   *\n" +
                   "************************************************");
        
        Console.WriteLine("1. Client");
        Console.WriteLine("2. Rentals");
        Console.WriteLine("3. Car management");
        Console.WriteLine("4. Exit");
    }
    
}