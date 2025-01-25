using LocationVoiture.dal.Entities;
using LocationVoiture.Present.View;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;

namespace LocationVoiture.Present
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _services;

        public MainWindow(IServiceProvider service)
        {
            InitializeComponent();
            this.DataContext = this;
            this._services = service;
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            ClientView v = _services.GetRequiredService<ClientView>();
            v.ShowDialog();
        }

        private void btnRentals_Click(object sender, RoutedEventArgs e)
        {
            RentalView v = _services.GetRequiredService<RentalView>();
            v.ShowDialog();
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            CarView v = _services.GetRequiredService<CarView>();
            v.ShowDialog();
        }
    }
}