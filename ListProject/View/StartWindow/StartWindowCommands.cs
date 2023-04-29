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
            Window window = new MainWindow();
            Presenter presenter = new Presenter(true);
            window.DataContext = presenter;
            Application.Current.Windows[0]?.Close();
            window.Show();
        });

        public ICommand NoClick => new DelegateCommand(() =>
        {
            Window window = new MainWindow();
            Presenter presenter = new Presenter();
            window.DataContext = presenter;
            Application.Current.Windows[0]?.Close();
            window.Show();
        });
    }
}