using System;
using System.Windows;
using warehouse_app.ViewModel;

namespace warehouse_app
{
    /// <summary>
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();

            var vm = new AddCategoryViewModel();
            DataContext = vm;
        }
    }
}
