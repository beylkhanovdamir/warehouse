using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using warehouse_app.DataAccess;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public abstract class WindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IWarehouseManager WarehouseManager => IocKernel.Get<IWarehouseManager>();

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly AppSettings _appSettings;

        public string AppTitle => _appSettings.AppTitle;

        protected WindowViewModel()
        {
            _appSettings = new AppSettings();
        }

        public static readonly ICommand SyncCommand =
        new RelayCommand(o => DataCache.Instance().Sync());
        
        public static readonly ICommand CloseCommand =
        new RelayCommand(o => ((Window)o).Close());
    }
}
