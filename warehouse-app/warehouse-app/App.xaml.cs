using System.Configuration;
using System.Runtime.Caching;
using System.Windows;
using warehouse_app.ViewModel;

namespace warehouse_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string FilesPath => ConfigurationManager.AppSettings.Get("jsonPath");

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IocKernel.Initialize(new CustomBindings());

            MainWindow view = new MainWindow { DataContext = new ProductViewModel() };
            view.Show();
        }
    }
}
