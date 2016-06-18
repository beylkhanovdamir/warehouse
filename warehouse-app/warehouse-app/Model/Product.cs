using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using warehouse_app.Annotations;
using System.ComponentModel.DataAnnotations;

namespace warehouse_app.Model
{
    public class Product : INotifyPropertyChanged
    {
        public Guid Id => Guid.NewGuid();
        private string _productName;
        private Category _category;

        [Required]
        public string ProductName {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
					_productName = value;
                    OnPropertyChanged(nameof(ProductName));
                }
            }
        }

        [Required]
        public Category Category {
            get { return _category; }
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
