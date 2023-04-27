using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using ListProject.View.SingleObjectWindow;
using Application = System.Windows.Application;
using Binding = System.Windows.Data.Binding;

namespace ListProject.ViewModel.Utils
{
    public class DataGridHandler : ObservableObject
    {
        public static bool testMode;
        public CustomDataGrid myDataGrid;

        public CustomDataGrid CreateDataGridFromGenericObjects(ObservableCollection<dynamic> objects,
            List<string>? propertyNamesToBeVisualized)
        {
            myDataGrid = new CustomDataGrid();
            myDataGrid.AutoGenerateColumns = false;

            (objects[0].GetType() as Type).GetProperties()
                .OrderByDescending(x => x.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                .Where(x =>
                {
                    return propertyNamesToBeVisualized?.Contains(x.Name, StringComparer.OrdinalIgnoreCase) ?? true;
                })
                .ToList().ForEach(propertyInfo =>
                    {
                        if (testMode || !propertyInfo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
                        {
                            var column = GenerateColumnFromPropertyInfo(propertyInfo);
                            myDataGrid.Columns.Add(column);
                        }
                    }
                );
            
            myDataGrid.ItemsSource = objects;
            CheckIfDataGridHasColumnsAndRows();
            PutRowStyleOnDataGridOnMouseOver();
            myDataGrid.PreviewMouseLeftButtonDown += MyDataGrid_PreviewMouseLeftButtonDown;
            return myDataGrid;
        }

        private void CheckIfDataGridHasColumnsAndRows()
        {
            if (myDataGrid.Items.Count == 0 || myDataGrid.Columns.Count == 0)
            {
                myDataGrid.NoDataTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                myDataGrid.NoDataTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private void PutRowStyleOnDataGridOnMouseOver()
        {
            Style rowStyle = new Style(typeof(DataGridRow));
            Trigger mouseOverTrigger = new Trigger() { Property = DataGridRow.IsMouseOverProperty, Value = true };
            mouseOverTrigger.Setters.Add(new Setter(DataGridRow.BackgroundProperty, Brushes.LightBlue));
            mouseOverTrigger.Setters.Add(new Setter(DataGridRow.ForegroundProperty, Brushes.White));
            rowStyle.Triggers.Add(mouseOverTrigger);
            myDataGrid.RowStyle = rowStyle;
        }

        private CustomDataGridColumn GenerateColumnFromPropertyInfo(PropertyInfo propertyInfo)
        {
            CustomDataGridColumn column = new CustomDataGridColumn();
            column.Header = propertyInfo.Name;
            column.Binding = new Binding(propertyInfo.Name);
            column.MyVisibility = Visibility.Visible;
            column.Width = DataGridLength.Auto;
            column.ColumnVisibilityChanged += OnColumnVisibilityChange;

            return column;
        }

        private void OnColumnVisibilityChange(object sender, EventArgs e)
        {
            foreach (var dataGridColumn in myDataGrid.Columns)
            {
                if ((dataGridColumn as CustomDataGridColumn).MyVisibility == Visibility.Visible)
                {
                    if (myDataGrid.Visibility != Visibility.Visible)
                    {
                        myDataGrid.Visibility = Visibility.Visible;
                        myDataGrid.NoDataTextBlock.Visibility = Visibility.Hidden;
                        RaisePropertyChangedEvent(myDataGrid.Name);
                    }

                    return;
                }
            }

            if (myDataGrid.Visibility != Visibility.Hidden)
            {
                myDataGrid.Visibility = Visibility.Hidden;
                RaisePropertyChangedEvent(myDataGrid.Name);
                myDataGrid.NoDataTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void MyDataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Get the clicked visual element
            var element = e.OriginalSource as FrameworkElement;

            // Navigate up the visual tree to find the clicked row
            while (element != null && !(element is DataGridRow))
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
            }

            // If a row was clicked, handle the event
            if (element is DataGridRow)
            {
                var row = (DataGridRow)element;
                var item = row.DataContext;
                // Get the type of the clicked row's data item
                Type itemType = item.GetType();

                // Construct the generic type of the SingleObjectPresenter class
                Type presenterType = typeof(SingleObjectPresenter<>).MakeGenericType(itemType);

                // Create an instance of the SingleObjectPresenter class with the correct generic type
                object presenter = Activator.CreateInstance(presenterType, item, testMode);
                foreach (Window window in Application.Current.Windows)
                {
                    // Check if the window is of the same type as the current window
                    if (window.GetType() == typeof(ObjectWindow))
                    {
                        // Close the window
                        window.Close();
                    }
                }

                ObjectWindow objectWindow = new ObjectWindow();
                objectWindow.DataContext = presenter;
                objectWindow.Show();
            }
        }
    }
}