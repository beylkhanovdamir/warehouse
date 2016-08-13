using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
			try
			{

				using (StreamWriter file = File.CreateText(GetJsonFileName<T>(modelType)))
				{
					var serializer = new JsonSerializer();
					serializer.Serialize(file, data);
				}
			}
			catch (Exception ex)
			{
				//todo add log
				//MessageBox.Show($"Ошибка. {ex.Message}; {ex.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private string GetJsonFileName<T>(ModelType modelType)
		{
			return Path.Combine(FilesPath, string.Concat(modelType.ToString().ToLower(), ".json"));
		}

		public IList<T> Load<T>(ModelType modelType)
		{
			if (!File.Exists(Path.Combine(FilesPath, string.Concat(modelType.ToString(), ".json"))))
			{
				return null;
			}
			var result = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(GetJsonFileName<T>(modelType)));
			return result;
		}
	}
}
