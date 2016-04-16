using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
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
            Products = new List<Product>()
            {
                new Product() { Category= new Category() {CategoryName = "Сигареты"}, Name = "Winston" },
                new Product() { Category= new Category() {CategoryName = "Хлеб"}, Name = "Городской батон" }
            };
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
            categoryWindow.Show();
        }


        #endregion

        #region Public props

        public IEnumerable<Category> Categories { get; set; }

        public Category SelectedCategory { get; set; }

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

        public IEnumerable<Product> Products { get; set; }

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
