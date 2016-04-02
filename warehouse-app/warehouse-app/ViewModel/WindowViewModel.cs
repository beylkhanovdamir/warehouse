using System.ComponentModel;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public abstract class WindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private readonly AppSettings _appSettings;

        public string AppTitle => _appSettings.AppTitle;

        protected WindowViewModel()
        {
            _appSettings = new AppSettings();
        }

    }
}
