using System.Windows;
using System.Windows.Controls;

namespace warehouse_app
{
	/// <summary>
	/// Interaction logic for AddOrder.xaml
	/// </summary>
	public partial class AddOrder : Window
	{
		public AddOrder()
		{
			InitializeComponent();
		}

		private void DataGrid_OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			if (e.Column.Header.ToString() == "Id")
			{ e.Column.Visibility = Visibility.Collapsed; }
		}
	}
}
