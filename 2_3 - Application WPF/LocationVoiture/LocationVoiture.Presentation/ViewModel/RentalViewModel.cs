using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using LocationVoiture.dal.Entities;
using LocationVoiture.Presentation.Core;
using LocationVoiture.bll.Services;

namespace LocationVoiture.Presentation.ViewModel
{
    public class RentalViewModel : INotifyPropertyChanged
    {
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;
        private readonly IClientService _clientService;
        private ObservableCollection<Rental> _rentals;
        private Rental _selectedRental;
        private ObservableCollection<Car> _cars;
        private ObservableCollection<Client> _clients;

        public int CarId { get; set; }
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int Duration { get; set; }
        public decimal Amount { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.reserved;

        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Rental> Rentals
        {
            get => _rentals;
            set
            {
                _rentals = value;
                OnPropertyChanged();
            }
        }

        public Rental SelectedRental
        {
            get => _selectedRental;
            set
            {
                _selectedRental = value;
                OnPropertyChanged();

                if (_selectedRental != null)
                {
                    CarId = _selectedRental.CarId;
                    ClientId = _selectedRental.ClientId;
                    StartDate = _selectedRental.StartDate;
                    Duration = _selectedRental.Duration;
                    Amount = _selectedRental.Amount;
                    Status = _selectedRental.Status;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand ReadCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand LoadClientsAndCarsCommand { get; }

        public RentalViewModel(IRentalService rentalService, ICarService carService, IClientService clientService)
        {
            _rentalService = rentalService;
            _carService = carService;
            _clientService = clientService;

            Rentals = new ObservableCollection<Rental>();
            Cars = new ObservableCollection<Car>();
            Clients = new ObservableCollection<Client>();

            CreateCommand = new RelayCommand(CreateRental);
            ReadCommand = new RelayCommand(ReadAllRentals);
            UpdateCommand = new RelayCommand(UpdateRental);
            DeleteCommand = new RelayCommand(DeleteRental);
            LoadClientsAndCarsCommand = new RelayCommand(LoadClientsAndCars);
        }

        private void LoadClientsAndCars(object parameter)
        {
            try
            {
                var clients = _clientService.GetAll();
                var cars = _carService.GetAll();

                Clients = new ObservableCollection<Client>(clients);
                Cars = new ObservableCollection<Car>(cars);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients and cars: {ex.Message}", "Load Error");
            }
        }

        private void CreateRental(object parameter)
        {
            if (CarId == 0 || ClientId == 0 || Duration <= 0 || Amount <= 0)
            {
                MessageBox.Show("All fields must be completed with valid values.", "Validation Error");
                return;
            }

            var newRental = new Rental
            {
                CarId = CarId,
                ClientId = ClientId,
                StartDate = StartDate,
                Duration = Duration,
                Amount = Amount,
                Status = Status
            };

            try
            {
                var createdRental = _rentalService.Create(newRental);
                Rentals.Add(createdRental);
                MessageBox.Show("Rental successfully created!", "Create Rental");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating rental: {ex.Message}", "Create Rental Error");
            }
        }

        private void ReadAllRentals(object parameter)
        {
            try
            {
                var rentalsFromService = _rentalService.GetAll();
                Rentals = new ObservableCollection<Rental>(rentalsFromService);
                MessageBox.Show("Rentals loaded successfully.", "Read Rentals");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rentals: {ex.Message}", "Read Rentals");
            }
        }

        private void UpdateRental(object parameter)
        {
            if (SelectedRental != null)
            {
                try
                {
                    var updatedRental = _rentalService.Update(SelectedRental);

                    int index = Rentals.IndexOf(SelectedRental);
                    if (index >= 0)
                    {
                        Rentals[index] = updatedRental;
                    }

                    MessageBox.Show("Rental updated successfully.", "Update Rental");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating rental: {ex.Message}", "Update Rental Error");
                }
            }
        }

        private void DeleteRental(object parameter)
        {
            if (SelectedRental != null)
            {
                try
                {
                    bool isDeleted = _rentalService.Delete(SelectedRental);
                    if (isDeleted)
                    {
                        Rentals.Remove(SelectedRental);
                        MessageBox.Show("Rental deleted successfully.", "Delete Rental");
                    }
                    else
                        MessageBox.Show("Error deleting rental.", "Delete Rental");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting rental: {ex.Message}", "Delete Rental Error");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}