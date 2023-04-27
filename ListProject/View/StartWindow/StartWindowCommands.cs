using System.Windows;
using System.Windows.Input;
using ListProject.View.AllObjectsWindow;
using ListProject.ViewModel.Utils;

namespace ListProject.View.StartWindow
{
    public class StartWindowCommands
    {
        public ICommand YesClick => new DelegateCommand(() =>
        {
            DataGridHandler.testMode = true;
            Window window = new MainWindow();
            Application.Current.Windows[0]?.Close();
            window.Show();
        });

        public ICommand NoClick => new DelegateCommand(() =>
        {
            DataGridHandler.testMode = false;
            Window window = new MainWindow();
            Application.Current.Windows[0]?.Close();
            window.Show();
        });
    }
}