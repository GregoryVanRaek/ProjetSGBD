using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using LocationVoiture.dal.Entities;
using LocationVoiture.Presentation;
using LocationVoiture.Presentation.Core;
using LocationVoiture.bll.Services;
using System.Windows.Controls;
using System.Diagnostics.Metrics;
using System.IO;

namespace LocationVoiture.Presentation.ViewModel
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
        public ICommand ReadCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ClientViewModel(IClientService clientService)
        {
            _clientService = clientService;

            Clients = new ObservableCollection<Client>();

            CreateCommand = new RelayCommand(CreateClient);
            ReadCommand = new RelayCommand(ReadAllClients);
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

            try
            {
                var createdClient = _clientService.Create(newClient);
                Clients.Add(createdClient);
                MessageBox.Show("Client successfully created!", "Create Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating client: {ex.Message}", "Create Client Error");
            }
        }

        private void ReadAllClients(object parameter)
        {
            try
            {
                var clientsFromService = _clientService.GetAll();
                Clients = new ObservableCollection<Client>(clientsFromService);
                MessageBox.Show("Clients loaded successfully.", "Read Clients");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading clients: {ex.Message}", "Read Clients");
            }
        }

        private void UpdateClient(object parameter)
        {
            if (SelectedClient != null)
            {
                if (string.IsNullOrWhiteSpace(SelectedClient.Firstname) || string.IsNullOrWhiteSpace(SelectedClient.Lastname))
                {
                    MessageBox.Show("Firstname and Lastname are required.", "Validation Error");
                    return;
                }

                try
                {
                    var updatedClient = _clientService.Update(SelectedClient);

                    int index = Clients.IndexOf(SelectedClient);
                    if (index >= 0)
                    {
                        Clients[index] = updatedClient;
                    }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
