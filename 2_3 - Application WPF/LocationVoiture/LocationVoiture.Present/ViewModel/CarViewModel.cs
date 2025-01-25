using LocationVoiture.bll.Services;
using LocationVoiture.dal.Entities;
using LocationVoiture.Present.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace LocationVoiture.Present.ViewModel
{
    public class CarViewModel : INotifyPropertyChanged
    {
        private readonly ICarService _carService;
        private readonly IModelService _modelService;

        private ObservableCollection<Car> _cars;
        private ObservableCollection<Model> _models;
        private ObservableCollection<Parking> _parkings;

        private Car _selectedCar;
        private string _licensePlate;
        private string _color;
        private int _modelId;
        private int _parkingId;

        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
                OnPropertyChanged();
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        public int ModelId
        {
            get => _modelId;
            set
            {
                _modelId = value;
                OnPropertyChanged();
            }
        }

        public int ParkingId
        {
            get => _parkingId;
            set
            {
                _parkingId = value;
                OnPropertyChanged();
            }
        }

        public Car SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged();

                if (_selectedCar != null)
                {
                    LicensePlate = _selectedCar.LicensePlate;
                    Color = _selectedCar.Color;
                    ModelId = (int)_selectedCar.ModelId;
                    ParkingId = (int)_selectedCar.ParkingId;
                }
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
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Parking> Parkings { get; set; }

        public ICommand CreateCarCommand { get; }
        public ICommand UpdateCarCommand { get; }
        public ICommand DeleteCarCommand { get; }

        public CarViewModel(ICarService carService, IModelService modelService)
        {
            _carService = carService;
            _modelService = modelService;

            LoadCars();
            LoadModels();
            LoadParkings();

            CreateCarCommand = new RelayCommand(CreateCar);
            UpdateCarCommand = new RelayCommand(UpdateCar);
            DeleteCarCommand = new RelayCommand(DeleteCar);
        }

        private void LoadCars()
        {
            try
            {
                Cars = new ObservableCollection<Car>(_carService.GetAll());
                OnPropertyChanged(nameof(Cars));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars: {ex.Message}", "Load Cars Error");
            }
        }

        private void LoadModels()
        {
            try
            {
                Models = new ObservableCollection<Model>(_modelService.GetAll());
                OnPropertyChanged(nameof(Models));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading models: {ex.Message}", "Load Models Error");
            }
        }

        private void LoadParkings()
        {
            try
            {
                Parkings = new ObservableCollection<Parking>(_carService.GetAllParking(true));
                OnPropertyChanged(nameof(Parkings));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading parkings: {ex.Message}", "Load Parkings Error");
            }
        }

        private void CreateCar(object parameter)
        {
            if (string.IsNullOrWhiteSpace(LicensePlate) ||
                string.IsNullOrWhiteSpace(Color) ||
                ParkingId == 0 ||
                ModelId == 0)
            {
                MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                return;
            }

            var newCar = new Car
            {
                LicensePlate = LicensePlate,
                Color = Color,
                ParkingId = ParkingId,
                ModelId = ModelId
            };

            try
            {
                _carService.Create(newCar);
                LoadCars();
                LoadParkings();
                ResetForm();
                MessageBox.Show("Car successfully created!", "Create Car");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating car: {ex.Message}", "Create Car Error");
            }
        }

        private void UpdateCar(object parameter)
        {
            if (SelectedCar == null)
            {
                MessageBox.Show("Please select a car to update.", "Update Car Error");
                return;
            }

            if (string.IsNullOrWhiteSpace(LicensePlate) ||
                string.IsNullOrWhiteSpace(Color) ||
                ParkingId == 0 ||
                ModelId == 0)
            {
                MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                return;
            }

            try
            {
                SelectedCar.LicensePlate = LicensePlate;
                SelectedCar.Color = Color;
                SelectedCar.ParkingId = ParkingId;
                SelectedCar.ModelId = ModelId;

                _carService.Update(SelectedCar);
                LoadCars();
                MessageBox.Show("Car updated successfully.", "Update Car");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating car: {ex.Message}", "Update Car Error");
            }
        }

        private void DeleteCar(object parameter)
        {
            if (SelectedCar == null)
            {
                MessageBox.Show("No car selected to delete.", "Delete Car Error");
                return;
            }

            try
            {
                bool isDeleted = _carService.Delete(SelectedCar);
                if (isDeleted)
                {
                    LoadCars();
                    LoadParkings();
                    MessageBox.Show("Car deleted successfully.", "Delete Car");
                }
                else
                {
                    MessageBox.Show("Error deleting car.", "Delete Car Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting car: {ex.Message}", "Delete Car Error");
            }
        }

        private void ResetForm()
        {
            LicensePlate = string.Empty;
            Color = string.Empty;
            ModelId = 0;
            ParkingId = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}