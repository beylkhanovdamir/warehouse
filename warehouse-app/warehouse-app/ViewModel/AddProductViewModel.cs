using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public class AddProductViewModel : WindowViewModel, IDataErrorInfo
    {
        private readonly Product _product;
        private DelegateCommand _saveProductCommand;

        public AddProductViewModel()
        {
            _product = new Product();
        }

        public ICommand SaveProductCommand => _saveProductCommand ??
                                               (_saveProductCommand = new DelegateCommand(ExecuteSaveProduct, CanSaveProduct));

        private bool CanSaveProduct()
        {
            return IsValid;
        }

        private void ExecuteSaveProduct()
        {
            WarehouseManager.Save(_product, ModelType.Products);
			Clear();
		}

        public string Name
        {
            get { return _product.ProductName; }
            set
            {
                _product.ProductName = value;
                OnPropertyChanged(nameof(Name));
                _saveProductCommand.RaiseCanExecuteChanged();
            }
		}

		private void Clear()
		{
			Name = string.Empty;
			_product.Category.CategoryName = String.Empty;
			SelectedCategory = null;
		}


		public Category SelectedCategory
		{
			get { return _product.Category; }
			set
			{
				_product.Category = value;
				OnPropertyChanged(nameof(SelectedCategory));
				_saveProductCommand.RaiseCanExecuteChanged();
			}
		}

		private string errorMessage = string.Empty;

        public string this[string columnName]
        {
            get
            {
                errorMessage = String.Empty;
                switch (columnName)
                {
                    case nameof(Name):
                        if (String.IsNullOrEmpty(_product.ProductName))
                        {
                            errorMessage = "Product Name is required";
                        }
                        break;

                }
                return errorMessage;
            }
        }

        public bool IsValid
        {
            get { return string.IsNullOrEmpty(this.Error); }
        }

        public string Error { get { return errorMessage; } }

        #region Other
        public AutoCompleteFilterPredicate<object> CategoryFilter
        {
            get
            {
                return (searchText, obj) =>
                {
                    var category = obj as Category;
                    return category != null && ((category.CategoryName.ToLower().Contains(searchText.ToLower())));
                };
            }
        }
        #endregion
    }
}
