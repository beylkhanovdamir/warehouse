using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
	public class AddOrderViewModel : WindowViewModel
	{
		private Order _order;
		private DelegateCommand _newOrderPositionCommand;
		private DelegateCommand _saveOrderCommand;
		private DelegateCommand _saveOrderPositionCommand;
		public ICommand NewOrderPositionCommand => _newOrderPositionCommand ??
								   (_newOrderPositionCommand = new DelegateCommand(ExecuteNewPosition, CanNewPosition));

		public ICommand SaveOrderPositionCommand => _saveOrderPositionCommand ??
						   (_saveOrderPositionCommand = new DelegateCommand(SavePosition, CanSavePosition));


		public AddOrderViewModel()
		{
			_order = new Order();
			_orderPositions = new ObservableCollection<OrderPosition>();
			SelectedPosition = new OrderPosition() { Overage = Convert.ToInt32(ConfigurationManager.AppSettings.Get("overage")) };
			SelectedPosition.PropertyChanged += SelectedPosition_PropertyChanged;
		}

		private void SelectedPosition_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			CheckExecute();
		}

		public Order Order
		{
			get { return _order; }
			set
			{
				_order = value;
				OnPropertyChanged(nameof(Order));
			}
		}

		private decimal _orderSum;

		public decimal OrderSum
		{
			get
			{
				return _orderSum;
			}
			set
			{
				_orderSum = value;
				OnPropertyChanged(nameof(OrderSum));
			}
		}

		private void UpdateSum()
		{
			OrderSum = OrderPositions.Sum(x => x.CostWithOverage);
		}

		private bool CanSavePosition()
		{
			return SaveIsAllowed;
		}

		public bool SaveIsAllowed { get; set; }

		public Dictionary<string, string> ErrorsList { get; set; }

		private void CheckErrors()
		{

			if (SelectedPosition.Qty <= 0)
			{
				string error;
				ErrorsList.TryGetValue("Qty", out error);
				if (error == null)
					ErrorsList.Add("Qty", "Кол-во обязательно для заполнения");
			}
			else
			{
				ErrorsList.Remove("Qty");
			}

			if (SelectedPosition.Product == null)
			{
				string error;
				ErrorsList.TryGetValue("Product", out error);
				if (error == null)
					ErrorsList.Add("Product", "Товар обязателен для заполнения");
			}
			else
			{
				ErrorsList.Remove("Product");
			}

			if (SelectedPosition.Category == null)
			{
				string error;
				ErrorsList.TryGetValue("Category", out error);
				if (error == null)
					ErrorsList.Add("Category", "Категория обязательна для заполнения");
			}
			else
			{
				ErrorsList.Remove("Category");
			}

			if (SelectedPosition.ProductUnit == null)
			{
				string error;
				ErrorsList.TryGetValue("ProductUnit", out error);
				if (error == null)
					ErrorsList.Add("ProductUnit", "Ед. измерения обязательна для заполнения");
			}
			else
			{
				ErrorsList.Remove("ProductUnit");
			}

			if (SelectedPosition.Cost <= 0)
			{
				string error;
				ErrorsList.TryGetValue("Cost", out error);
				if (error == null)
					ErrorsList.Add("Cost", "Цена не может быть отрицательной или нулевой");
			}
			else
			{
				ErrorsList.Remove("Cost");
			}

			if (SelectedPosition.Overage < 0)
			{
				string error;
				ErrorsList.TryGetValue("Overage", out error);
				if (error == null)
					ErrorsList.Add("Overage", "Наценка не может быть отрицательной");
			}
			else
			{
				ErrorsList.Remove("Overage");
			}

			SaveIsAllowed = !ErrorsList.Any();
		}

		private void SavePosition()
		{
			var exist = OrderPositions.FirstOrDefault(x => x.Id == SelectedPosition.Id);
			if (exist != null)
			{
				exist.ProductUnit = SelectedPosition.ProductUnit;
				exist.Product = SelectedPosition.Product;
				exist.Category = SelectedPosition.Category;
				exist.Qty = SelectedPosition.Qty;
				exist.Overage = SelectedPosition.Overage;
				exist.Cost = SelectedPosition.Cost;
				exist.CostWithOverage = SelectedPosition.CostWithOverage;
			}

			SelectedPosition = new OrderPosition
			{
				ProductUnit = Units.ProductUnits[0],
				Product = null,
				Category = null,
				Cost = 0,
				Qty = 0,
				Overage = Convert.ToInt32(ConfigurationManager.AppSettings.Get("overage"))
			};

			CheckExecute();

			UpdateSum();
		}

		private bool CanNewPosition()
		{
			return !OrderPositions.Contains(SelectedPosition) && SaveIsAllowed;
		}

		private void ExecuteNewPosition()
		{
			OrderPositions.Add(SelectedPosition);
			SelectedPosition = new OrderPosition
			{
				ProductUnit = Units.ProductUnits[0],
				Product = null,
				Category = null,
				Cost = 0,
				Qty = 0,
				Overage = Convert.ToInt32(ConfigurationManager.AppSettings.Get("overage"))
			};
			SelectedPosition.PropertyChanged += SelectedPosition_PropertyChanged;

			CheckExecute();

			UpdateSum();
		}

		private void CheckExecute()
		{
			CheckErrors();
			if (OrderPositions.Contains(SelectedPosition))
				_saveOrderPositionCommand.RaiseCanExecuteChanged();
			if (!OrderPositions.Contains(SelectedPosition))
				_newOrderPositionCommand.RaiseCanExecuteChanged();
		}

		public ICommand SaveOrderCommand => _saveOrderCommand ??
											   (_saveOrderCommand = new DelegateCommand(ExecuteSaveOrder, CanSaveOrder));

		private bool CanSaveOrder()
		{
			return OrderPositions.Contains(SelectedPosition) && SaveIsAllowed;
		}

		private OrderPosition _selectedPosition;
		public OrderPosition SelectedPosition
		{
			get { return _selectedPosition; }
			set
			{
				if (value != null)
				{
					_selectedPosition = value;
					OnPropertyChanged(nameof(SelectedPosition));
					ErrorsList = new Dictionary<string, string>();
					if (OrderPositions.Contains(SelectedPosition))
					{
						SelectedPosition.PropertyChanged += SelectedPosition_PropertyChanged;
						CheckExecute();
					}
				}
			}
		}

		private void ExecuteSaveOrder()
		{
			// todo: OrderPositions get all positions
			WarehouseManager.Save(_order, ModelType.Orders);
		}

		public string OrderNumber => $"Накладная № {_order.Id}";


		public string OrderDate => _order.OrderDate.ToString("d");

		private ObservableCollection<OrderPosition> _orderPositions;

		public ObservableCollection<OrderPosition> OrderPositions
		{
			get
			{
				return _orderPositions;
			}
			set
			{
				_orderPositions = value;
				OnPropertyChanged(nameof(OrderPositions));
			}
		}
	}
}

