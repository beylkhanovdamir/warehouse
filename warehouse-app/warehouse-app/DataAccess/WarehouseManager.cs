using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
    public class WarehouseFileManager: IWarehouseManager
    {
        public string FilesPath => ConfigurationManager.AppSettings.Get("jsonPath");

        public WarehouseFileManager()
        {
            CheckPath();
        }

        private void CheckPath()
        {
            if (!Directory.Exists(FilesPath))
            {
                Directory.CreateDirectory(FilesPath);
            }
        }

        public void SaveCategory(Category category)
        {
            using (StreamWriter file = File.CreateText(Path.Combine(FilesPath, "categories.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, category);
            }
        }
    }
}
