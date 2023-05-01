using System;
using System.Windows;
using System.Windows.Controls;

namespace ListProject.ViewModel.Utils
{
    public class CustomDataGridColumn : DataGridTextColumn
    {
        public event EventHandler? ColumnVisibilityChanged;

        public Visibility MyVisibility
        {
            get { return Visibility; }
            set
            {
                if (Visibility != value)
                {
                    Visibility = value;
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