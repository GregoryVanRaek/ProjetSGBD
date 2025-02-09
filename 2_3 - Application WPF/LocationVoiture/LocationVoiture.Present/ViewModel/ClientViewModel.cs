using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using LocationVoiture.dal.Entities;
using LocationVoiture.Present.Core;
using LocationVoiture.bll.Services;
using System.Text.RegularExpressions;


namespace LocationVoiture.Present.ViewModel
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        private readonly IClientService _clientService;
        private ObservableCollection<Client> _clients;
        private Client _selectedClient;

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string License { get; set; }
        public DateTime? BirthDate { get; set; }

        private bool _isFormValid;
        public bool IsFormValid
        {
            get => _isFormValid;
            set
            {
                _isFormValid = value;
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

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged();

                if (_selectedClient != null)
                {
                    Street = _selectedClient.Address?.Street;
                    PostalCode = _selectedClient.Address?.PostalCode;
                    City = _selectedClient.Address?.City;
                    Country = _selectedClient.Address?.Country;
                }
            }
        }
        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ClientViewModel(IClientService clientService)
        {
            _clientService = clientService;

            Clients = new ObservableCollection<Client>(_clientService.GetAll());

            CreateCommand = new RelayCommand(CreateClient);
            UpdateCommand = new RelayCommand(UpdateClient);
            DeleteCommand = new RelayCommand(DeleteClient);
        }

        private void CreateClient(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Firstname) || string.IsNullOrWhiteSpace(Lastname) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Street) || string.IsNullOrWhiteSpace(PostalCode) || string.IsNullOrWhiteSpace(City) ||
                string.IsNullOrWhiteSpace(Country) || string.IsNullOrWhiteSpace(License))
            {
                MessageBox.Show("All fields must be completed.", "Validation Error");
                return;
            } 

            var newClient = new Client
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Email = Email,
                Address = new Address
                {
                    Street = Street,
                    PostalCode = PostalCode,
                    City = City,
                    Country = Country
                },
                DrivingLicense = License,
                BirthDate = BirthDate ?? DateTime.Now // Si la date de naissance est null, utilise la date actuelle.
            };

            if (!IsValidEmail(newClient.Email))
            {
                MessageBox.Show("Incorrect email address", "Validation Error");
                return;
            }

            try
            {
                var createdClient = _clientService.Create(newClient);
                Clients.Add(createdClient);
                ResetForm();
                MessageBox.Show("Client successfully created!", "Create Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating client: {ex.Message}", "Create Client Error");
            }
        }

        private void ResetForm()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
            Email = string.Empty;
            Street = string.Empty;
            PostalCode = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            License = string.Empty;
            BirthDate = null;
            OnPropertyChanged(nameof(Firstname));
            OnPropertyChanged(nameof(Lastname));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Street));
            OnPropertyChanged(nameof(PostalCode));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(License));
            OnPropertyChanged(nameof(BirthDate));
        }

        private void UpdateClient(object parameter)
        {
            if (SelectedClient != null)
            {
                if (string.IsNullOrWhiteSpace(SelectedClient.Firstname) || string.IsNullOrWhiteSpace(SelectedClient.Lastname) ||
                                   string.IsNullOrWhiteSpace(SelectedClient.Email) || string.IsNullOrWhiteSpace(SelectedClient.Address?.Street) ||
                                   string.IsNullOrWhiteSpace(SelectedClient.Address?.PostalCode) || string.IsNullOrWhiteSpace(SelectedClient.Address?.City) ||
                                   string.IsNullOrWhiteSpace(SelectedClient.Address?.Country) || string.IsNullOrWhiteSpace(SelectedClient.DrivingLicense))
                {
                    MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                    return;
                }

                if (!IsValidEmail(SelectedClient.Email))
                {
                    MessageBox.Show("Incorrect email address", "Validation Error");
                    return;
                }

                try
                {
                    var updatedClient = _clientService.Update(SelectedClient);


                    MessageBox.Show("Client updated successfully.", "Update Client");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating client: {ex.Message}", "Update Client Error");
                }
            }
        }

        private void DeleteClient(object parameter)
        {
            if (SelectedClient != null)
            {
                var confirmationResult = MessageBox.Show(
                                                "Are you sure you want to delete this client?",
                                                "Confirm delete",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question
                );
                if (confirmationResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool isDeleted = _clientService.Delete(SelectedClient);
                        if (isDeleted)
                        {
                            Clients.Remove(SelectedClient);
                            MessageBox.Show("Client deleted successfully.", "Delete Client");
                        }
                        else
                            MessageBox.Show("Error deleting client.", "Delete Client");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting client: {ex.Message}", "Delete Client Error");
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsValidEmail(string email)
        {
            var reg = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, reg);
        }

    }

}
