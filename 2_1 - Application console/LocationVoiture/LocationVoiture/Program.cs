using LocationVoiture.bll.Services;
using LocationVoiture.dal;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories;
using LocationVoiture.Presentation;
using Microsoft.Extensions.DependencyInjection;

#region Configuration

// Configuration

var services = new ServiceCollection();

// Dependency injection
    // BLL
    services.AddScoped<ICarService, CarService>();
    services.AddScoped<IClientService, ClientService>();
    // DAL
    services.AddSingleton<DBAccess>();
    services.AddScoped<ICarRepository, CarRepository>();
    services.AddScoped<IClientRepository, ClientRepository>();

var serviceProvider = services.BuildServiceProvider();

#endregion

#region Main

HomeController home = new HomeController();
home.MainMenu();

#endregion

#region Test

ICarService carService = serviceProvider.GetService<ICarService>();

List<Car> cars = carService.GetAll();

foreach (var car in cars)
{
    Console.WriteLine(car.ToString());
}

#endregion
