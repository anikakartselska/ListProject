using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace ListProject.ViewModel.Presenters
{
    public class SingleObjectPresenter
    {
        private StackPanel _myStackPanel;

        public StackPanel MyStackPanel
        {
            get => _myStackPanel;
            set => _myStackPanel = value;
        }

        public SingleObjectPresenter(dynamic obj)
        {
            MyStackPanel = CreateStackPanel(obj);
        }

        private StackPanel CreateStackPanel(dynamic obj)
        {
            StackPanel panel = new StackPanel();
            panel.GetType().GetProperties().ToList()
                .ForEach(property =>
                    {
                        Label label = new Label();
                        label.Content = property + ":" + obj.GetType().GetProperty(property).GetValue(obj);

                        StackPanel propertyStackPanel = new StackPanel();
                        propertyStackPanel.Orientation = Orientation.Horizontal;
                        propertyStackPanel.Children.Add(label);

                        panel.Children.Add(propertyStackPanel);
                    }
                );
            return panel;
        }
    }
}