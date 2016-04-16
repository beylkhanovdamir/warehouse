using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace warehouse_app.Model
{
    public class Category : INotifyPropertyChanged
    {
        public Guid Id => Guid.NewGuid();

        private string _categoryName;

        [Required]
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
