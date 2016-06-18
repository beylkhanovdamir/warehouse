using System;
using System.Collections.Generic;

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

		public ProductUnit Unit { get; set; }

		public decimal Cost { get; set; }
	}
}
