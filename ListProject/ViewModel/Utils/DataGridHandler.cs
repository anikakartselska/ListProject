using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ListProject.View.SingleObjectWindow;
using ListProject.ViewModel.Presenters;
using Application = System.Windows.Application;
using Binding = System.Windows.Data.Binding;

namespace ListProject.ViewModel.Utils
{
    public class DataGridHandler : ObservableObject
    {
        private CustomDataGrid? _myDataGrid;
        private List<string>? _propertiesToBeVisualized;

        public CustomDataGrid? MyDataGrid
        {
            get => _myDataGrid;
            set => _myDataGrid = value;
        }

        public List<string>? PropertiesToBeVisualized
        {
            get => _propertiesToBeVisualized;
            set => _propertiesToBeVisualized = value;
        }

        public CustomDataGrid? CreateDataGridFromGenericObjects(ObservableCollection<dynamic>? objects,
            List<string>? propertiesToBeVisualized)
        {
            MyDataGrid = new CustomDataGrid();
            MyDataGrid.AutoGenerateColumns = false;
            PropertiesToBeVisualized = (propertiesToBeVisualized ??
                                           (objects?[0].GetType() as Type)?.GetProperties()
                                           .Select(property => property.Name)!)
                .OrderByDescending(propertyName => propertyName.Equals("Id", StringComparison.OrdinalIgnoreCase))
                .ToList();

            PropertiesToBeVisualized
                .ToList().ForEach(propertyInfo =>
                    {
                        var column = GenerateColumnFromPropertyInfo(propertyInfo);
                        MyDataGrid.Columns.Add(column);
                    }
                );

            MyDataGrid.ItemsSource = objects;
            CheckIfDataGridHasColumnsAndRows();
            PutRowStyleOnDataGridOnMouseOver();
            MyDataGrid.PreviewMouseLeftButtonDown += MyDataGrid_PreviewMouseLeftButtonDown;
            return MyDataGrid;
        }

        private void CheckIfDataGridHasColumnsAndRows()
        {
            if (MyDataGrid != null && (MyDataGrid.Items.Count == 0 || MyDataGrid.Columns.Count == 0))
            {
                MyDataGrid.NoDataTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                MyDataGrid!.NoDataTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void PutRowStyleOnDataGridOnMouseOver()
        {
            Style rowStyle = new Style(typeof(DataGridRow));
            Trigger mouseOverTrigger = new Trigger() { Property = UIElement.IsMouseOverProperty, Value = true };
            mouseOverTrigger.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.LightBlue));
            mouseOverTrigger.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.White));
            rowStyle.Triggers.Add(mouseOverTrigger);
            MyDataGrid!.RowStyle = rowStyle;
        }

        private CustomDataGridColumn GenerateColumnFromPropertyInfo(string propertyName)
        {
            CustomDataGridColumn column = new CustomDataGridColumn();
            column.Header = propertyName;
            column.Binding = new Binding(propertyName);
            column.MyVisibility = Visibility.Visible;
            column.Width = DataGridLength.Auto;
            column.ColumnVisibilityChanged += OnColumnVisibilityChange;

            return column;
        }

        private void OnColumnVisibilityChange(object sender, EventArgs e)
        {
            foreach (var dataGridColumn in MyDataGrid?.Columns!)
            {
                if ((dataGridColumn as CustomDataGridColumn)!.MyVisibility == Visibility.Visible)
                {
                    if (MyDataGrid.Visibility != Visibility.Visible)
                    {
                        MyDataGrid.Visibility = Visibility.Visible;
                        MyDataGrid.NoDataTextBlock.Visibility = Visibility.Hidden;
                        RaisePropertyChangedEvent(MyDataGrid.Name);
                    }

                    return;
                }
            }

            if (MyDataGrid.Visibility != Visibility.Hidden)
            {
                MyDataGrid.Visibility = Visibility.Hidden;
                RaisePropertyChangedEvent(MyDataGrid.Name);
                MyDataGrid.NoDataTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void MyDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;

            while (element != null && !(element is DataGridRow))
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }


            if (element != null)
            {
                var row = (DataGridRow)element;
                var item = row.DataContext;

                SingleObjectPresenter singleObjectPresenter =
                    new SingleObjectPresenter(item, PropertiesToBeVisualized!);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(ObjectWindow))
                    {
                        window.Close();
                    }
                }

                ObjectWindow objectWindow = new ObjectWindow();
                objectWindow.DataContext = singleObjectPresenter;
                objectWindow.Show();
            }
        }
    }
}