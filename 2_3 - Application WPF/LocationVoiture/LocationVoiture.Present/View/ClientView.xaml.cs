using LocationVoiture.Present.ViewModel;
using System.Windows;


namespace LocationVoiture.Present.View
{
    /// <summary>
    /// Interaction logic for ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        public ClientView(ClientViewModel clientViewModel)
        {
            InitializeComponent();
            this.DataContext = clientViewModel;
        }
    }
}
