using LocationVoiture.Present.ViewModel;
using System.Windows;


namespace LocationVoiture.Present.View
{
    /// <summary>
    /// Interaction logic for RentalView.xaml
    /// </summary>
    public partial class RentalView : Window
    {
        public RentalView(RentalViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
