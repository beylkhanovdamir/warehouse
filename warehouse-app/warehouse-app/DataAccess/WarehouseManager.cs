using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
    public class WarehouseFileManager : IWarehouseManager
    {
        public string FilesPath => ((App)Application.Current).FilesPath;

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

        public void Save<T>(T model, ModelType modelType)
        {
            var data = Load<T>(modelType);

            if (data != null)
            {
                data.Add(model);
            }
            else
            {
                data = new List<T>() { model };
            }

            using (StreamWriter file = File.CreateText(Path.Combine(FilesPath, string.Concat(modelType.ToString(), ".json"))))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }

        public IList<T> Load<T>(ModelType modelType)
        {
            if (!File.Exists(Path.Combine(FilesPath, string.Concat(modelType.ToString(), ".json"))))
            {
                return null;
            }
            var result = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(Path.Combine(FilesPath, string.Concat(modelType.ToString(), ".json"))));
            return result;
        }
    }
}
