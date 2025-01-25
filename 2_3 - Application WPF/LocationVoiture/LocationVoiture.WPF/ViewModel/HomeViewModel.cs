using LocationVoiture.WPF.Core;
using LocationVoiture.WPF.Services;
using LocationVoiture.WPF.View;
using System.Windows.Input;

namespace LocationVoiture.WPF.ViewModel
{
    public class HomeViewModel
    {
        private readonly NavigationService _navigationService;

        public ICommand NavigateToClientViewCommand { get; }
        public ICommand NavigateToCarViewCommand { get; }
        public ICommand NavigateToRentalViewCommand { get; }

        public HomeViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToClientViewCommand = new RelayCommand(NavigateToClientView);
            NavigateToCarViewCommand = new RelayCommand(NavigateToCarView);
            NavigateToRentalViewCommand = new RelayCommand(NavigateToRentalView);
        }

        private void NavigateToClientView() => _navigationService.Navigate<ClientView>();
        private void NavigateToCarView() => _navigationService.Navigate<CarView>();
        private void NavigateToRentalView() => _navigationService.Navigate<RentalView>();
    }
}
