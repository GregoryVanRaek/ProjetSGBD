using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using LocationVoiture.dal.Entities;
using LocationVoiture.bll.Services;
using LocationVoiture.Present.Core;

namespace LocationVoiture.Present.ViewModel
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
        private ObservableCollection<Rental> _currentRentals;


        public int ClientId { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.reserved;

        private int _carId;
        public int CarId
        {
            get => _carId;
            set
            {
                _carId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set
            {
                _cars = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Rental> CurrentRentals
        {
            get => _currentRentals;
            set
            {
                _currentRentals = value;
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

        private Car _selectedCar;
        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                CarId = _selectedCar?.Id ?? 0;
                CalculateAmount();
                OnPropertyChanged();
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                CalculateAmount();
                OnPropertyChanged();
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        private void CalculateAmount()
        {
            if (CarId > 0 && Duration > 0)
            {
                Amount = _carService.CalculateRentalAmount(CarId, Duration);
            }
            else
            {
                Amount = 0;
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                ClientId = _selectedClient?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        public ICommand ReadCommand { get; }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EncloseCommand { get; }

        public RentalViewModel(IRentalService rentalService, ICarService carService, IClientService clientService)
        {
            _rentalService = rentalService;
            _carService = carService;
            _clientService = clientService;

            Cars = new ObservableCollection<Car>(_carService.GetAll());
            Clients = new ObservableCollection<Client>(_clientService.GetAll());

            ReadCommand = new RelayCommand(ReadAllRentals);
            CreateCommand = new RelayCommand(CreateRental);
            UpdateCommand = new RelayCommand(UpdateRental);
            DeleteCommand = new RelayCommand(DeleteRental);
            EncloseCommand = new RelayCommand(Enclose);

            ReadAllRentals(null);
            UpdateRentalState();
        }

        private void ReadAllRentals(object parameter)
        {
            try
            {
                var rentalsFromService = _rentalService.GetAll();
                Rentals = new ObservableCollection<Rental>(rentalsFromService.Select(rental =>
                {
                    var client = _clientService.GetById(rental.ClientId);
                    var car = _carService.GetById(rental.CarId);

                    rental.ClientFullName = $"{client?.Firstname} {client?.Lastname}";
                    rental.CarInfo = $"{car?.ModelName} ({car?.LicensePlate})";
                    return rental;
                }));
                CurrentRentals = new ObservableCollection<Rental>(Rentals.Where(r => r.Status == RentalStatus.rent));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading rentals: {ex.Message}", "Read Rentals Error");
            }
        }

        private void CreateRental(object parameter)
        {
            if (CheckDuplicateRental(CarId, StartDate, Duration))
            {
                MessageBox.Show("This car is already rented or reserved for the selected dates.");
                return;
            }

            var newRental = new Rental
            {
                CarId = CarId,
                ClientId = ClientId,
                StartDate = StartDate,
                Duration = Duration,
                Amount = Amount,
                Status = StartDate > DateTime.Today ? RentalStatus.reserved : RentalStatus.rent
            };

            try
            {
                if (newRental.Status == RentalStatus.rent)
                    _carService.UpdateCarParking(newRental.CarId);

                var createdRental = _rentalService.Create(newRental);
                ReadAllRentals(null);

                MessageBox.Show("Rental successfully created!", "Create Rental");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating rental: {ex.Message}", "Create Rental Error");
            }
        }

        private void UpdateRental(object parameter)
        {
            if (SelectedRental != null)
            {
                if (CheckDuplicateRental(CarId, StartDate, Duration))
                {
                    MessageBox.Show("This car is already rented or reserved for the selected dates.");
                    return;
                }

                try
                {
                    SelectedRental.Amount = _carService.CalculateRentalAmount(SelectedRental.CarId, SelectedRental.Duration);
                    var updatedRental = _rentalService.Update(SelectedRental);
                    int index = Rentals.IndexOf(SelectedRental);
                    if (index >= 0)
                    {
                        Rentals[index] = updatedRental;
                    }
                    this.ReadAllRentals(parameter);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting rental: {ex.Message}", "Delete Rental Error");
                }
            }
        }

        private void Enclose(object parameter)
        {
            if (SelectedRental != null)
            {
                try
                {
                    SelectedRental.Status = RentalStatus.completed;
                    _rentalService.Update(SelectedRental);
                    this.ReadAllRentals(null);
                    MessageBox.Show("Rental successfully enclosed.", "Enclose Rental");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error enclosing rental: {ex.Message}", "Enclose Rental Error");
                }
            }
        }

        public bool CheckDuplicateRental(int carId, DateTime startDate, int duration)
        {
            var rentals = _rentalService.GetAll().Where(r => r.CarId == carId);
            DateTime endDate = startDate.AddDays(duration);
            return rentals.Any(r => (r.Status == RentalStatus.rent || r.Status == RentalStatus.reserved) &&
                                    (startDate < r.StartDate.AddDays(r.Duration) &&
                                     endDate > r.StartDate &&
                                    r.Id != SelectedRental?.Id));
        }
        public void UpdateRentalState()
        {
            var rentals = _rentalService.GetAll();
            foreach (var rental in rentals)
            {
                if (rental.Status == RentalStatus.reserved && rental.StartDate <= DateTime.Now)
                {
                    rental.Status = RentalStatus.rent;
                    _rentalService.Update(rental);
                    _carService.UpdateCarParking(rental.CarId);
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