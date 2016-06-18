using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
	public class AddOrderViewModel : WindowViewModel, IDataErrorInfo
	{
		private Order _order;
		private DelegateCommand _newOrderPositionCommand;
		private DelegateCommand _saveOrderCommand;

		public AddOrderViewModel()
		{
			_order = new Order();
			OrderPositions = new List<OrderPosition>();
			SelectedPosition = new OrderPosition() { Overage = Convert.ToInt32(ConfigurationManager.AppSettings.Get("overage"))};
		}

		public Order Order
		{
			get { return _order; }
			set
			{
				_order = value;
				OnPropertyChanged(nameof(Order));
				_newOrderPositionCommand.RaiseCanExecuteChanged();
			}
		}

		public ICommand NewOrderPositionCommand => _newOrderPositionCommand ??
								   (_newOrderPositionCommand = new DelegateCommand(ExecuteNewPosition, CanNewPosition));

		private bool CanNewPosition()
		{
			return IsValid;
		}

		private void ExecuteNewPosition()
		{
			OrderPositions.Add(SelectedPosition);
		}

		public ICommand SaveOrderCommand => _saveOrderCommand ??
											   (_saveOrderCommand = new DelegateCommand(ExecuteSaveOrder, CanSaveOrder));

		private bool CanSaveOrder()
		{
			return IsValid;
		}

		private OrderPosition selectedPosition;
		public OrderPosition SelectedPosition
		{
			get { return selectedPosition; }
			set
			{
				selectedPosition = value;
				OnPropertyChanged(nameof(SelectedPosition));
			}
		}

		private void ExecuteSaveOrder()
		{
			// todo: OrderPositions get all positions
			WarehouseManager.Save(_order, ModelType.Orders);
		}

		public string OrderNumber => $"Накладная № {_order.Id}";


		public string OrderDate => _order.OrderDate.ToString("d");

		public List<OrderPosition> OrderPositions { get; set; }

		private string errorMessage = string.Empty;

		public string this[string columnName]
		{
			get
			{
				errorMessage = String.Empty;
				switch (columnName)
				{
					case nameof(OrderDate):
						break;

				}
				return errorMessage;
			}
		}

		public bool IsValid
		{
			get { return string.IsNullOrEmpty(this.Error); }
		}

		public string Error { get { return errorMessage; } }
	}
}

