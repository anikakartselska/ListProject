using System;
using System.Windows;
using System.Windows.Controls;

namespace ListProject.ViewModel.Utils
{
    public class CustomDataGridColumn : DataGridTextColumn
    {
        private Visibility _myVisibility = Visibility.Visible;

        public event EventHandler ColumnVisibilityChanged;

        public Visibility MyVisibility
        {
            get { return _myVisibility; }
            set
            {
                if (_myVisibility != value)
                {
                    this.Visibility = value;
                    _myVisibility = value;
                    OnColumnVisibilityChanged();
                }
            }
        }

        private void OnColumnVisibilityChanged()
        {
            ColumnVisibilityChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}