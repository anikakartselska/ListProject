#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ListProject.ViewModel.Utils;

namespace ListProject.ViewModel.Presenters
{
    public class BasePresenter : ObservableObject
    {
        private ObservableCollection<dynamic> _objects;
        private CustomDataGrid _myDataGrid;
        private List<string> _propertiesToBeVisualized;

        public CustomDataGrid MyDataGrid
        {
            get => _myDataGrid;
            set
            {
                _myDataGrid = value;
                RaisePropertyChangedEvent(nameof(MyDataGrid));
            }
        }

        public List<string> PropertiesBeVisualized
        {
            get => _propertiesToBeVisualized;
            set
            {
                _propertiesToBeVisualized = value;
                RaisePropertyChangedEvent(nameof(PropertiesBeVisualized));
            }
        }

        public ObservableCollection<dynamic> Objects
        {
            get => _objects;
            set
            {
                _objects = value;
                RaisePropertyChangedEvent(nameof(Objects));
            }
        }

        public BasePresenter(ObservableCollection<dynamic> objects, List<string>? propertiesToBeVisualized)
        {
            SetObjectsAndPropertiesAndChangeDataGrid(objects, propertiesToBeVisualized);
        }

        public BasePresenter()
        {
        }

        public void SetObjectsAndPropertiesAndChangeDataGrid(ObservableCollection<dynamic> objects,
            List<string>? propertiesToBeVisualized)
        {
            Objects = objects;
            PropertiesBeVisualized = propertiesToBeVisualized ??
                                     (objects[0].GetType() as Type).GetProperties().Select(info => info.Name).ToList();
            MyDataGrid = new DataGridHandler().CreateDataGridFromGenericObjects(Objects, PropertiesBeVisualized);
        }
    }
}