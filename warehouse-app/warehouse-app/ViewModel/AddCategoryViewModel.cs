using System;
using System.Collections.Generic;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public class AddCategoryViewModel : WindowViewModel
    {
        private readonly Category _category;

        public AddCategoryViewModel()
        {
            _category = new Category();
        }

        public string Name
        {
            get { return _category.Name; }
            set { _category.Name = value; }
        }
    }
}
