using LocationVoiture.bll.Services;
using LocationVoiture.dal;
using LocationVoiture.dal.Entities;
using LocationVoiture.dal.Repositories;
using LocationVoiture.dal.Repositories.Interface;
using LocationVoiture.Presentation;
using Microsoft.Extensions.DependencyInjection;

#region Configuration

// Injection de dépendances
var services = new ServiceCollection();

    // Controllers
    services.AddScoped<HomeController>();
    services.AddScoped<ClientController>();
    services.AddScoped<CarController>();
    services.AddScoped<RentController>();
    services.AddScoped<CategoryController>();

    // BLL
    services.AddScoped<ICarService, CarService>();
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<ICategoryService, CategoryService>();

    // DAL
    services.AddSingleton<DBAccess>();
    services.AddScoped<ICarRepository, CarRepository>();
    services.AddTransient<IClientRepository, ClientRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

#endregion

#region Main

HomeController homeController = serviceProvider.GetRequiredService<HomeController>();
homeController.MainMenu(serviceProvider);

#endregion

#region test

#endregion
