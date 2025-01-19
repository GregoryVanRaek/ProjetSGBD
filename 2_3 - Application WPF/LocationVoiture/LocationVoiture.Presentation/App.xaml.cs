using LocationVoiture.dal.Repositories.Interface;
using LocationVoiture.dal.Repositories;
using LocationVoiture.dal;
using LocationVoiture.Presentation.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using LocationVoiture.Presentation.View;
using LocationVoiture.bll.Services;

namespace LocationVoiture.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            // Configuration des services (injection de dépendances)
            var services = new ServiceCollection();

            // pages
            services.AddSingleton<MainWindow>();

            // Vues et ViewModels
            services.AddScoped<ClientsView>();
            services.AddScoped<ClientViewModel>();

            services.AddScoped<CarView>();
            services.AddScoped<CarViewModel>();

            services.AddScoped<RentalView>();
            services.AddScoped<RentalViewModel>();

            // Controllers
            services.AddScoped<HomeController>();
            services.AddScoped<ClientController>();
            services.AddScoped<CarController>();
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
