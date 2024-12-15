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
    services.AddScoped<ModelController>();
    services.AddScoped<RentalController>();

    // BLL
    services.AddScoped<ICarService, CarService>();
    services.AddScoped<IClientService, ClientService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IModelService, ModelService>();
    services.AddScoped<IRentalService, RentalService>();

    // DAL
    services.AddSingleton<DBAccess>();
    services.AddScoped<ICarRepository, CarRepository>();
    services.AddScoped<IClientRepository, ClientRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IModelRepository, ModelRepository>();
    services.AddScoped<IRentalRepository, RentalRepository>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

#endregion

#region Main

HomeController homeController = serviceProvider.GetRequiredService<HomeController>();
homeController.MainMenu(serviceProvider);

#endregion

#region test

#endregion
