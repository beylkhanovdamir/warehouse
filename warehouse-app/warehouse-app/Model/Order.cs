using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.Prism.Commands;

namespace warehouse_app.Model
{
	public class Order
	{
		public Guid Id => Guid.NewGuid();

		public DateTime OrderDate => DateTime.Now;

		public List<OrderPosition> OrderPositions { get; set; }
	}

	public class OrderPosition : INotifyPropertyChanged
	{
		public OrderPosition()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }

		private Product _product;
		[Required]
		public Product Product
		{
			get { return _product; }
			set
			{
				_product = value;
				OnPropertyChanged(nameof(Product));
			}
		}

		private Category _category;

		[Required]
		public Category Category
		{
			get { return _category; }
			set
			{
				_category = value;
				OnPropertyChanged(nameof(Category));
			}
		}

		private int _qty;
		[Required]
		public int Qty
		{
			get { return _qty; }
			set
			{
				_qty = value;
				OnPropertyChanged(nameof(Qty));
				UpdateFinalCost();
			}
		}

		private ProductUnit _productUnit;
		[Required]
		public ProductUnit ProductUnit
		{
			get { return _productUnit; }
			set
			{
				_productUnit = value;
				OnPropertyChanged(nameof(ProductUnit));
			}
		}

		private decimal _cost;

		[Required]
		public decimal Cost
		{
			get
			{
				return _cost;
			}
			set
			{
				_cost = value;
				OnPropertyChanged(nameof(Cost));
				UpdateFinalCost();
			}
		}

		private int _overage;
		[Required]
		[Range(0, 100)]
		public int Overage
		{
			get
			{
				return _overage;
			}
			set
			{
				_overage = value;
				OnPropertyChanged(nameof(Overage));
				UpdateFinalCost();
			}
		}

		private decimal _costWithOverage;

		public decimal CostWithOverage
		{
			get { return _costWithOverage; }
			set
			{
				_costWithOverage = value;
				OnPropertyChanged(nameof(CostWithOverage));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		public void UpdateFinalCost()
		{
			CostWithOverage = Qty * Cost * Overage / 100;
		}
	}

	public class ProductUnit
	{
		public string UnitName { get; set; }
	}


	
}
