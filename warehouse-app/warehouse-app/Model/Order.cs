using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace warehouse_app.Model
{
	public class Order
	{
		public Guid Id => Guid.NewGuid();

		public DateTime OrderDate => DateTime.Now;

		public List<OrderPosition> OrderPositions { get; set; }
	}

	public class OrderPosition
	{
		public Guid Id { get; set; }

		public Product Product { get; set; }

		public Category Category { get; set; }

		public int Qty { get; set; }

		public List<ProductUnit> Units => GetUnits();

		private List<ProductUnit> GetUnits()
		{
			var units = ConfigurationManager.AppSettings.Get("units").Split(',').Select(x => new ProductUnit() { UnitName = x }).ToList();
			return units;
		}

		public ProductUnit ProductUnit { get; set; }

		public decimal Cost { get; set; }

		public int Overage { get; set; }
	}

	public class ProductUnit
	{
		public string UnitName { get; set; }
	}
}
