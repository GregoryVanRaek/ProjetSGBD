using LocationVoiture.bll.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

using System.Windows;


namespace LocationVoiture.Present.View
{
    /// <summary>
    /// Interaction logic for CarView.xaml
    /// </summary>
    public partial class CarView : Window
    {
        private readonly IServiceProvider _service;
        public CarView(IServiceProvider service)
        {
            InitializeComponent();
            this._service = service;
            this.DataContext = this;
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            CarDetailView v = this._service.GetRequiredService<CarDetailView>();
            v.ShowDialog();
        }

        private void btnModels_Click(object sender, RoutedEventArgs e)
        {
            ModelView v = this._service.GetRequiredService<ModelView>();
            v.ShowDialog();
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryView v = this._service.GetRequiredService<CategoryView>();
            v.ShowDialog();
        }
    }
}
