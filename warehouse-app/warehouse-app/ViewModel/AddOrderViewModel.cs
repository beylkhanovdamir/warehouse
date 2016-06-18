using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
	public class AddOrderViewModel : WindowViewModel, IDataErrorInfo
	{
		private readonly Order _order;
		private List<OrderPosition> _orderPositions;
		private DelegateCommand _newOrderPositionCommand;
		private DelegateCommand _saveOrderCommand;

		public AddOrderViewModel()
		{
			_order = new Order();
		}


		public ICommand NewOrderPositionCommand => _newOrderPositionCommand ??
								   (_newOrderPositionCommand = new DelegateCommand(ExecuteNewPosition, CanNewPosition));

		private bool CanNewPosition()
		{
			return IsValid;
		}

		private void ExecuteNewPosition()
		{
			WarehouseManager.Save(_order, ModelType.Orders);
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
				_newOrderPositionCommand.RaiseCanExecuteChanged();
			}
		}

		private Product _selectedProduct;
		public Product SelectedProduct
		{
			get { return SelectedPosition.Product; }
			set
			{
				_selectedProduct = value;
				OnPropertyChanged(nameof(_selectedProduct));
				_newOrderPositionCommand.RaiseCanExecuteChanged();
			}
		}

		private void ExecuteSaveOrder()
		{
			WarehouseManager.Save(_order, ModelType.Orders);
		}

		public string OrderNumber => $"Накладная № {_order.Id}";


		public string OrderDate => _order.OrderDate.ToString(CultureInfo.InvariantCulture);

		public List<OrderPosition> OrderPositions
		{
			get { return _orderPositions; }
			set
			{
				_orderPositions = value;
				OnPropertyChanged(nameof(OrderPositions));
				_saveOrderCommand.RaiseCanExecuteChanged();
			}
		}

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

