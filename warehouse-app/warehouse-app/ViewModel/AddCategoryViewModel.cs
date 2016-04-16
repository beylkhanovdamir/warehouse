﻿using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Ninject;
using warehouse_app.DataAccess;
using warehouse_app.Model;

namespace warehouse_app.ViewModel
{
    public class AddCategoryViewModel : WindowViewModel, IDataErrorInfo
    {
        private readonly Category _category;
        private DelegateCommand _saveCategoryCommand;
        public IWarehouseManager WarehouseManager => IocKernel.Get<IWarehouseManager>();

        public AddCategoryViewModel()
        {
            _category = new Category();
        }

        public ICommand SaveCategoryCommand => _saveCategoryCommand ??
                                               (_saveCategoryCommand = new DelegateCommand(ExecuteSaveCategory, CanSaveCategory));

        private bool CanSaveCategory()
        {
            return IsValid;
        }

        private void ExecuteSaveCategory()
        {
            WarehouseManager.SaveCategory(_category);
        }

        public string Name
        {
            get { return _category.CategoryName; }
            set
            {
                _category.CategoryName = value;
                OnPropertyChanged("Name");
                _saveCategoryCommand.RaiseCanExecuteChanged();
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
                    case nameof(Name):
                        if (String.IsNullOrEmpty(_category.CategoryName))
                        {
                            errorMessage = "Category Name is required";
                        }
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
