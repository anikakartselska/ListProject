using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ListProject.Model;
using ListProject.View.StartWindow;
using ListProject.ViewModel;
using ListProject.ViewModel.Utils;

namespace ListProject.View.AllObjectsWindow
{
    public class Presenter : BasePresenter
    {
        private List<Type> _types;
        private Type _selectedType;

        public Type SelectedType
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

        private void OnSelectedItemChanged(Type entityType)
        {
            if (entityType != null)
            {
                var dynamicEntitiesList = new MyContextService().GetEntitiesListFromDatabaseByType(entityType);
                Objects = new ObservableCollection<dynamic>(dynamicEntitiesList);
            }
        }

        public Presenter()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Entity));
            Types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Entity))).ToList();
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
    }
}