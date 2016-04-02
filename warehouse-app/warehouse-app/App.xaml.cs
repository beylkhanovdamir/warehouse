using System.Windows;

namespace warehouse_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow view = new MainWindow { DataContext = new ViewModel.ProductViewModel() };
            view.Show();
        }
    }
}
