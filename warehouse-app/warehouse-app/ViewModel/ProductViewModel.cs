using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using warehouse_app.DataAccess;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public class ProductViewModel : WindowViewModel
    {
        #region Private Members
        private readonly Product _product;
        //
        private DelegateCommand _addProductCommand;
        private DelegateCommand _addCategoryCommand;


        #endregion

        #region ctor

        public ProductViewModel()
        {
			this._product = new Product();
			DataCache.Instance().Sync();
			Products = DataCache.Instance().Products;
		}
        #endregion

        #region Commands

        public ICommand AddProductCommand
        {
            get
            {
                if (_addProductCommand == null)
                {
                    _addProductCommand = new DelegateCommand(ExecuteAddProduct, CanExecuteAddProduct);
                }
                return _addProductCommand;
            }
        }

        private bool CanExecuteAddProduct()
        {
            // todo
            return true;
        }

        private void ExecuteAddProduct()
        {
            // todo
            var vm = new AddProductViewModel();
            AddProductWindow productWindow = new AddProductWindow { DataContext = vm };
            productWindow.ShowDialog();
        }

        public ICommand AddCategoryCommand
        {
            get
            {
                if (_addCategoryCommand == null)
                {
                    _addCategoryCommand = new DelegateCommand(ExecuteAddCategory, CanExecuteAddCategory);
                }
                return _addCategoryCommand;
            }
        }

        private bool CanExecuteAddCategory()
        {
            // todo
            return true;
        }

        private void ExecuteAddCategory()
        {
            // todo
            var vm = new AddCategoryViewModel();
            AddCategoryWindow categoryWindow = new AddCategoryWindow { DataContext = vm };
            categoryWindow.ShowDialog();
        }


        #endregion

        #region Public props

        public string Name
        {
            get { return _product.Name; }
            set { _product.Name = value; }
        }

        public Category Category
        {
            get { return _product.Category; }
            set { _product.Category = value; }
        }

	    public IEnumerable<Product> Products { get; }

        public Product SelectedProduct { get; set; }

        #endregion

        #region Other
        public AutoCompleteFilterPredicate<object> ProductFilter
        {
            get
            {
                return (searchText, obj) =>
                {
                    var product = obj as Product;
                    return product != null && (product.Name.ToLower().Contains(searchText.ToLower()) || (product.Category.CategoryName.ToLower().Contains(searchText.ToLower())));
                };
            }
        }
        #endregion

    }
}
