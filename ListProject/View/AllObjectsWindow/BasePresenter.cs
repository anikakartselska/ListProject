#nullable enable
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ListProject.ViewModel.Utils;

namespace ListProject.View.AllObjectsWindow
{
    public class BasePresenter : ObservableObject
    {
        private ObservableCollection<dynamic> _objects;
        private CustomDataGrid _myDataGrid;
        private List<string> _propertyNamesToBeVisualized;

        public CustomDataGrid MyDataGrid
        {
            get => _myDataGrid;
            set
            {
                _myDataGrid = value;
                RaisePropertyChangedEvent(nameof(MyDataGrid));
            }
        }

        public List<string> PropertyNamesToBeVisualized
        {
            get => _propertyNamesToBeVisualized;
            set => _propertyNamesToBeVisualized = value;
        }

        public ObservableCollection<dynamic> Objects
        {
            get => _objects;
            set
            {
                _objects = value;
                RaisePropertyChangedEvent(nameof(Objects));
                MyDataGrid = new DataGridHandler().CreateDataGridFromGenericObjects(_objects,null);
            }
        }

        public BasePresenter(ObservableCollection<dynamic> objects,List<string>? propertyNamesToBeVisualized)
        {
            _objects = objects;
            _propertyNamesToBeVisualized = propertyNamesToBeVisualized;
            _myDataGrid = new DataGridHandler().CreateDataGridFromGenericObjects(Objects,propertyNamesToBeVisualized);
        }

        public BasePresenter()
        {
        }
    }
}