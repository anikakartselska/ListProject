using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using ListProject.Model.Db;
using ListProject.Model.Entities;
using ListProject.View.StartWindow;
using ListProject.ViewModel.Utils;

namespace ListProject.View.AllObjectsWindow
{
    public class Presenter : BasePresenter
    {
        private List<Type> _types;
        private Type? _selectedType;
        private bool _testMode;

        public bool TestMode
        {
            get => _testMode;
            set => _testMode = value;
        }

        public Type? SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                RaisePropertyChangedEvent(nameof(SelectedType));
                OnSelectedItemChanged(_selectedType);
            }
        }

        public List<Type> Types
        {
            get => _types;
            set => _types = value;
        }

        private void OnSelectedItemChanged(Type? entityType)
        {
            if (entityType != null)
            {
                var dynamicEntitiesList = new MyContextService().GetEntitiesListFromDatabaseByType(entityType);
                if (!TestMode)
                {
                    PropertiesBeVisualized = FilterIdPropertyAndGetObjectPropertyNames(entityType);
                }
                Objects = new ObservableCollection<dynamic>(dynamicEntitiesList);
            }
        }

        public Presenter()
        {
            Types = InitializeObjectsTypes();
        }

        public Presenter(bool testMode) : this()
        {
            TestMode = testMode;
        }

        public ICommand ClearListAndSelection => new DelegateCommand(() =>
        {
            SelectedType = null;
            MyDataGrid = null;
        });

        public static ICommand GoBackToStartScreen => new DelegateCommand(() =>
        {
            Window window = new StartScreen();
            Application.Current.Windows[0]?.Close();
            window.Show();
        });

        private List<Type> InitializeObjectsTypes()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Entity));
            return assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Entity))).ToList();
        }

        private List<string> FilterIdPropertyAndGetObjectPropertyNames(Type? entityType)
        {
            return entityType?.GetProperties()
                .Where(x => !x.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                .Select(info => info.Name)
                .ToList() ?? new List<string>();
        }
    }
}