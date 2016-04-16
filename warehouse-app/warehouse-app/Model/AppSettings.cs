using System.Configuration;

namespace warehouse_app.Model
{
    public class AppSettings
    {
        public string AppTitle
        {
            get
            {
                var title = ConfigurationManager.AppSettings.Get("appTitle");
                return title;
            }
        }
    }
}
