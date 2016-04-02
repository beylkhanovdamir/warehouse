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
        private DelegateCommand _saveProductCommand;

        #endregion

        #region ctor

        public ProductViewModel()
        {
            this._product = new Product();
        }
        #endregion

        #region Commands

        public ICommand SaveProductCommand
        {
            get
            {
                if (_saveProductCommand == null)
                {
                    _saveProductCommand = new DelegateCommand(ExecuteSaveProduct, CanExecuteSaveProduct);
                }
                return _saveProductCommand;
            }
        }

        private bool CanExecuteSaveProduct()
        {
            // todo
            return true;
        }

        private void ExecuteSaveProduct()
        {
            // todo
        }


        #endregion

        #region Public props

        public string Name
        {
            get { return _product.Name; }
            set { _product.Name = value; }
        }

        #endregion

    }
}
