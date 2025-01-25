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
    public class CategoryViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryService _categoryService;
        private ObservableCollection<Category> _categories;
        private Category _selectedCategory;

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

        private decimal _dailyRate;
        public decimal DailyRate
        {
            get => _dailyRate;
            set
            {
                _dailyRate = value;
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

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();

                if (_selectedCategory != null)
                {
                    Id = _selectedCategory.Id;
                    Name = _selectedCategory.Name;
                    DailyRate = _selectedCategory.DailyRate;
                }
            }
        }



        public ICommand CreateCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            LoadCategories();

            CreateCommand = new RelayCommand(CreateCategory);
            UpdateCommand = new RelayCommand(UpdateCategory);
            DeleteCommand = new RelayCommand(DeleteCategory);
        }

        private void LoadCategories()
            {
                Categories = new ObservableCollection<Category>(_categoryService.GetAll());
            }

        private void CreateCategory(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Name) || DailyRate <= 0)
            {
                MessageBox.Show("All fields must be completed correctly.", "Validation Error");
                return;
            }

            var newCategory = new Category
            {
                Name = Name,
                DailyRate = DailyRate
            };

            try
            {
                var createdCategory = _categoryService.Create(newCategory);
                LoadCategories();
                MessageBox.Show("Category successfully created!", "Create Category");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating category: {ex.Message}", "Create Category Error");
            }
        }

        private void UpdateCategory(object parameter)
        {
            if (SelectedCategory != null)
            {
                if (string.IsNullOrWhiteSpace(Name) || DailyRate <= 0)
                {
                    MessageBox.Show("Name and Daily Rate are required.", "Validation Error");
                    return;
                }

                try
                {
                    _categoryService.Update(SelectedCategory);
                    LoadCategories();
                    MessageBox.Show("Category updated successfully.", "Update Category");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating category: {ex.Message}", "Update Category Error");
                }
            }
        }

        private void DeleteCategory(object parameter)
        {
            if (SelectedCategory != null)
            {
                try
                {
                    bool isDeleted = _categoryService.Delete(SelectedCategory);
                    if (isDeleted)
                    {
                        LoadCategories();
                        MessageBox.Show("Category deleted successfully.", "Delete Category");
                    }
                    else
                        MessageBox.Show("Error deleting category.", "Delete Category");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}", "Delete Category Error");
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
