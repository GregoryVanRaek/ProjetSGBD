using System;
using System.Windows.Controls;

namespace LocationVoiture.WPF.Services
{
    public class NavigationService
    {
        private readonly Frame _frame;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(Frame frame, IServiceProvider serviceProvider)
        {
            _frame = frame;
            _serviceProvider = serviceProvider;
        }

        public void Navigate<T>() where T : Page
        {
            var page = (T)_serviceProvider.GetService(typeof(T));
            _frame.Navigate(page);
        }
        public void GoBack()
        {
            if (_frame.CanGoBack)
                _frame.GoBack();
        }
    }
}
