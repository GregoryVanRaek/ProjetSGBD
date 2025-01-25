using LocationVoiture.bll.Services;
using LocationVoiture.dal.Entities;
using LocationVoiture.Present.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace LocationVoiture.Present.ViewModel
{
    public class ModelViewModel : INotifyPropertyChanged
    {
        private readonly IModelService _modelService;
        private readonly ICategoryService _categoryService;
        private ObservableCollection<Model> _models;
        private Model _selectedModel;

        private ObservableCollection<Category> _categories; 
        private int _categoryId;
        public int CategoryId
        {
            get => _categoryId;
            set
            {
                _categoryId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories 
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _brand;
        public string Brand
        {
            get => _brand;
            set
            {
                _brand = value;
                OnPropertyChanged();
            }
        }

        private int _seatNumber;
        public int SeatNumber
        {
            get => _seatNumber;
            set
            {
                _seatNumber = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Model> Models
        {
            get => _models;
            set
            {
                _models = value;
                OnPropertyChanged();
            }
        }

        public Model SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                OnPropertyChanged();

                if (_selectedModel != null)
                {
                    Id = _selectedModel.Id;
                    Name = _selectedModel.Name;
                    Brand = _selectedModel.Brand;
                    SeatNumber = _selectedModel.SeatNumber;
                    CategoryId = _selectedModel.CategoryId;
                }
            }
        }

        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ModelViewModel(IModelService modelService, ICategoryService categoryService)
        {
            _modelService = modelService;
            _categoryService = categoryService;
            Models = new ObservableCollection<Model>(_modelService.GetAll());
            Categories = new ObservableCollection<Category>(_categoryService.GetAll()); 

            CreateCommand = new RelayCommand(CreateModel);
            UpdateCommand = new RelayCommand(UpdateModel);
            DeleteCommand = new RelayCommand(DeleteModel);
        }

        private void LoadModels()
        {
            try
            {
                Models = new ObservableCollection<Model>(_modelService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading models: {ex.Message}", "Load Models Error");
            }
        }

        private void CreateModel(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Brand) || SeatNumber < 2)
            {
                MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                return;
            }

            var newModel = new Model
            {
                Name = Name,
                Brand = Brand,
                SeatNumber = SeatNumber,
                CategoryId = CategoryId
            };

            try
            {
                var createdModel = _modelService.Create(newModel);
                LoadModels();
                MessageBox.Show("Model successfully created!", "Create Model");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating model: {ex.Message}", "Create Model Error");
            }
        }

        private void UpdateModel(object parameter)
        {
            if (SelectedModel != null)
            {
                if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Brand) || SeatNumber < 2)
                {
                    MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                    return;
                }

                try
                {
                    _modelService.Update(SelectedModel);
                    LoadModels();
                    MessageBox.Show("Model updated successfully.", "Update Model");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating model: {ex.Message}", "Update Model Error");
                }
            }
        }

        private void DeleteModel(object parameter)
        {
            if (SelectedModel != null)
            {
                try
                {
                    bool isDeleted = _modelService.Delete(SelectedModel);
                    if (isDeleted)
                    {
                        LoadModels();
                        MessageBox.Show("Model deleted successfully.", "Delete Model");
                    }
                    else
                    {
                        MessageBox.Show("Error deleting model.", "Delete Model");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting model: {ex.Message}", "Delete Model Error");
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