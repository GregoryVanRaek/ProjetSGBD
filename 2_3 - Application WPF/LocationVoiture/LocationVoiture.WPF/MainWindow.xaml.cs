using LocationVoiture.WPF.Services;
using LocationVoiture.WPF.View;
using LocationVoiture.WPF.ViewModel;
using System.Windows;


namespace LocationVoiture.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NavigationService _navigationService;

        public MainWindow(NavigationService navigationService)
        {
            InitializeComponent();
            _navigationService = navigationService;

            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _navigationService.Navigate<HomeView>();
        }
    }
}