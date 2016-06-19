using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;

namespace warehouse_app.Model
{
	public static class Units
	{
		public static ObservableCollection<ProductUnit> ProductUnits => new ObservableCollection<ProductUnit>(ConfigurationManager.AppSettings.Get("units").Split(',').Select(x => new ProductUnit() { UnitName = x }).ToList());
	}
}
