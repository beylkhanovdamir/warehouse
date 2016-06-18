using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Windows;
using warehouse_app.DataAccess;
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
			DataCache.Instance.Sync();
			MainWindow view = new MainWindow { DataContext = new ProductViewModel() };
            view.Show();
        }
    }
}
