using LocationVoiture.bll.Services;
using LocationVoiture.dal;
using LocationVoiture.dal.Repositories;
using LocationVoiture.dal.Repositories.Interface;
using LocationVoiture.Present.View;
using LocationVoiture.Presentation;
using LocationVoiture.Present.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace LocationVoiture.Present
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Register views
            services.AddSingleton<MainWindow>();
            services.AddTransient<ClientView>();
            services.AddTransient<CarView>();
            services.AddTransient<RentalView>();
            services.AddTransient<CarDetailView>();
            services.AddTransient<CategoryView>();
            services.AddTransient<ModelView>();

            // Register ViewModels
            services.AddTransient<ClientViewModel>();
            services.AddTransient<CarViewModel>();
            services.AddTransient<RentalViewModel>();
            services.AddTransient<CategoryViewModel>();
            services.AddTransient<ModelViewModel>();

            // Register controllers
            services.AddScoped<HomeController>();
            services.AddScoped<ClientController>();
            services.AddScoped<CarController>();
            services.AddScoped<CategoryController>();
            services.AddScoped<ModelController>();
            services.AddScoped<RentalController>();

            // Register BLL services
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IRentalService, RentalService>();

            // Register DAL
            services.AddSingleton<DBAccess>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
