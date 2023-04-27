using System.Windows.Controls;

namespace ListProject.ViewModel.Utils
{
    public class CustomDataGrid : DataGrid
    {
        private TextBlock _noDataTextBlock = new TextBlock() { Text = "No data available." };

        public TextBlock NoDataTextBlock
        {
            get => _noDataTextBlock;
        }
    }
}