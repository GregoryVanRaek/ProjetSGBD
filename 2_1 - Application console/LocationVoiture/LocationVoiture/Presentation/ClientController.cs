using LocationVoiture.bll.Services;
using LocationVoiture.dal.CustomException;
using LocationVoiture.dal.Entities;

namespace LocationVoiture.Presentation;

public class ClientController
{
    private readonly IClientService _clientService;
    
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    public void ClientMenu()
    {
        int choice = 0;
        
        while (choice != 5)
        {
            DefaultDisplay();
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
            switch (choice)
            {
                case 1 : GetAllClient();
                    break;
                case 2 : GetOneClient();
                    break;
                case 3 : Console.WriteLine("Update client");
                    break;
                case 4 : Console.WriteLine("Add client");
                    break;
                case 5 : Console.WriteLine("back to menu");
                    break;
                default : Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
    
    #region Crud
    
    private void GetAllClient()
    {
        ConsoleAccess.CreateScreen("All client");
        DisplayHeader();
        try
        { 
            List<Client> clients = _clientService.GetAll();
            DisplayClient(clients);
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void GetOneById(int id)
    {
        try
        { 
            Client? client = _clientService.GetById(id);

            if (client is not null)
            {
                ConsoleAccess.CreateScreen($"Client {client.Id}");
                DisplayClient(client);
            }
            else
            {
                Console.WriteLine("Client not found");
            }
            
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void GetOneByName(string name)
    {
        try
        { 
            Client? client = _clientService.GetOneByName(name);

            if (client is not null)
            {
                ConsoleAccess.CreateScreen($"Client {client.Id}");
                DisplayClient(client);
            }
            else
            {
                Console.WriteLine("Client not found");
            }
            
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private void GetOneClient()
    {
        ConsoleAccess.CreateScreen("Client research");
        try
        {
            int researchType = ConsoleAccess.ReadInput<int>("Would you like to search by id (1) or by name (2) ? ");

            switch (researchType)
            {
                case 1 : GetOneById(ConsoleAccess.ReadInput<int>("Enter client's id : "));
                    break;
                case 2 : GetOneByName(ConsoleAccess.ReadInput<string>("Enter client's name : "));
                    break;
                default: Console.WriteLine("Invalid choice");
                    ConsoleAccess.Wait();
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid choice" + e.Message);
        }
    }
    
    #endregion
    
    #region private methods
    private void DefaultDisplay()
    {
        ConsoleAccess.CreateScreen("Client menu");
        Console.WriteLine("1. All clients");
        Console.WriteLine("2. Search by name");
        Console.WriteLine("3. Update client");
        Console.WriteLine("4. Add new client");
        Console.WriteLine("5. Back to main menu");
    }

    private void DisplayHeader()
    {
        Console.WriteLine("ID".PadRight(5) + 
                          "Last Name".PadRight(15) + 
                          "First Name".PadRight(15) + 
                          "Email".PadRight(30) + 
                          "Street".PadRight(20) + 
                          "PostalCode".PadRight(12) + 
                          "City".PadRight(15) + 
                          "Country".PadRight(15) + 
                          "License");
        Console.WriteLine(new string('-', 130));
    }
    
    private void DisplayClient(Client client)
    {
        Console.WriteLine(client.Id.ToString().PadRight(5) +
                          client.Lastname.PadRight(15) +
                          client.Firstname.PadRight(15) +
                          client.Email.PadRight(30) +
                          client.Address.Street.PadRight(20) +
                          client.Address.PostalCode.PadRight(12) +
                          client.Address.City.PadRight(15) +
                          client.Address.Country.PadRight(15) +
                          client.DrivingLicense);
    }

    private void DisplayClient(List<Client> clients)
    {
        foreach (var client in clients)
            DisplayClient(client);
    }
    
    #endregion
    
}