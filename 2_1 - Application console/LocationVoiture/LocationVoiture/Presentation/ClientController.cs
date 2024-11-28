using System.Text.RegularExpressions;
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
        
        while (choice != 6)
        {
            DisplayOptions();
            choice = ConsoleAccess.ReadInput<int>("Enter your choice : ");
            switch (choice)
            {
                case 1 : GetAllClient();
                    break;
                case 2 : GetOneClient();
                    break;
                case 3 : UpdateClient();
                    break;
                case 4 : CreateClient();
                    break;
                case 5 : DeleteClient();
                    break;
                case 6 : Console.WriteLine("back to menu");
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
                DisplayHeader();
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
                DisplayHeader();
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

    private Client? CreateClient()
    {
        Client? newClient;
        string lastname = "", firstname = "", email = "", street = "", postalCode = "", city = "", country = "", drivingLicense = "";
        DateTime birthdate = new DateTime();
        bool emailSuccess ;
        
        ConsoleAccess.CreateScreen("Create new client");
        
        try
        {
            lastname = CheckString(lastname, "Lastname");
            firstname = CheckString(firstname, "Firstname");
            
            do
            {
                email = ConsoleAccess.ReadInput<string>("Email : ");
                emailSuccess = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").Match(email).Success;
                if(!emailSuccess)
                    Console.WriteLine("Please enter a valid email address");
            } while (!emailSuccess);
            
            street = CheckString(street, "Street");
            postalCode = CheckString(postalCode, "Postal Code : ");
            city = CheckString(city, "City : ");
            country = CheckString(country, "Country : ");
            drivingLicense = CheckString(drivingLicense, "Driving license : ");
            
            while (!DateTime.TryParse(ConsoleAccess.ReadInput<string>("Birthdate : "), out birthdate))
                Console.WriteLine("Invalid date format. Please try again.");
            
            newClient = new Client
            {
                Lastname = lastname,
                Firstname = firstname,
                Email = email,
                Address = new Address
                {
                    Street = street,
                    PostalCode = postalCode,
                    City = city,
                    Country = country
                },
                DrivingLicense = drivingLicense,
                BirthDate = birthdate
            };

            Client? c = this._clientService.Create(newClient);

            if (c is not null)
            {
                Console.WriteLine("Client created successfully");
                DisplayHeader();
                DisplayClient(c);
            }
            else
                Console.WriteLine("An error occured during the creation of the client");
            
            ConsoleAccess.Wait();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            ConsoleAccess.Wait();
        }
        return null;
    }

    private Client? UpdateClient()
    {
        int id;
        bool emailSuccess;
        DateTime birthdate;
        ConsoleAccess.CreateScreen("Update client");

        try
        {
            do
            {
                id = ConsoleAccess.ReadInput<int>("Enter client's id : ");
            } while (id <= 0);
        
            Client? client = _clientService.GetById(id);

            if (client is not null)
            {
                DisplayHeader();
                DisplayClient(client);
                
                client.Lastname = CheckString(client.Lastname, "Lastname");
                client.Firstname = CheckString(client.Firstname, "Firstname");
            
                do
                {
                    client.Email = ConsoleAccess.ReadInput<string>("Email : ");
                    emailSuccess = new Regex("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$").Match(client.Email).Success;
                    if(!emailSuccess)
                        Console.WriteLine("Please enter a valid email address");
                } while (!emailSuccess);
            
                client.Address.Street = CheckString(client.Address.Street, "Street");
                client.Address.PostalCode = CheckString(client.Address.PostalCode, "Postal Code : ");
                client.Address.City = CheckString(client.Address.City, "City : ");
                client.Address.Country = CheckString(client.Address.Country, "Country : ");
                client.DrivingLicense = CheckString(client.DrivingLicense, "Driving license : ");
                
                
                while (!DateTime.TryParse(ConsoleAccess.ReadInput<string>("Birthdate : "), out birthdate))
                    Console.WriteLine("Invalid date format. Please try again.");
            
                client.BirthDate = birthdate;
                
                DisplayClient(client);
                return this._clientService.Update(client);
            }
            else
            {
                Console.WriteLine("Client not found");
                return null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private bool DeleteClient()
    {
        int id;
        
        ConsoleAccess.CreateScreen("Delete client");
        
        do
        {
            id = ConsoleAccess.ReadInput<int>("Enter client's id : ");
        } while (id <= 0);

        try
        {
            Client? client = _clientService.GetById(id);

            if (client is not null)
            {
                DisplayHeader();
                DisplayClient(client);
                Console.Write("Are you sure you want to delete this client? (y/n)");
                string choice = Console.ReadLine().ToString().ToLower();
                switch (choice)
                {
                    case "y" : _clientService.Delete(client);
                        Console.WriteLine("Client deleted");
                        ConsoleAccess.Wait();
                        return true;
                    case "n": break;
                    default: Console.WriteLine("Invalid choice");
                        break;
                }
            }
            else
                Console.WriteLine("Client not found");
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return false;
    }
    
    #endregion
    
    #region private methods
    private void DisplayOptions()
    {
        ConsoleAccess.CreateScreen("Client menu");
        Console.WriteLine("1. All clients");
        Console.WriteLine("2. Research one client");
        Console.WriteLine("3. Update client");
        Console.WriteLine("4. Add new client");
        Console.WriteLine("5. Delete client");
        Console.WriteLine("6. Back to main menu");
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

    private string CheckString(string value, string fieldName)
    {
        do
        {
            value = ConsoleAccess.ReadInput<string>($"{fieldName} : ");
            if(string.IsNullOrEmpty(value.Trim()))
                Console.WriteLine($"{fieldName} is required");
        } while (string.IsNullOrEmpty(value.Trim()));

        return value.ToLower();
    }
    
    #endregion
    
}