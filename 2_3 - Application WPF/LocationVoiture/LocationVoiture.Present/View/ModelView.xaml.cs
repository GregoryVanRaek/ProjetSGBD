using LocationVoiture.Present.ViewModel;
using System.Windows;

namespace LocationVoiture.Present.View
{
    /// <summary>
    /// Interaction logic for ModelView.xaml
    /// </summary>
    public partial class ModelView : Window
    {
        public ModelView(ModelViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
