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
        private string _name;
        private Category _category;

        [Required]
        public string Name {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
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
