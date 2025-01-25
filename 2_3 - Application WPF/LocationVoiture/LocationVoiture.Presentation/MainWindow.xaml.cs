using LocationVoiture.Presentation.View;
using LocationVoiture.Presentation.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocationVoiture.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object ClientView { get; }
        public object CarView { get; }
        public object RentalView { get; }

        public MainWindow(ClientsView clientView, ClientViewModel clientViewModel,
                  CarView carView, CarViewModel carViewModel,
                  RentalView rentalView, RentalViewModel rentalViewModel)
        {
            InitializeComponent();

            // Associer les ViewModels aux Vues
            clientView.DataContext = clientViewModel;
            carView.DataContext = carViewModel;
            rentalView.DataContext = rentalViewModel;

            // Rendre les vues accessibles via le binding
            ClientView = clientView;
            CarView = carView;
            RentalView = rentalView;

            DataContext = this;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CarView c = new CarView();
            c.ShowDialog();
        }
    }
}