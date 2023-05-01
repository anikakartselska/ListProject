using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        public SingleObjectPresenter(dynamic obj, List<string> propertiesToBeVisualized)
        {
            MyStackPanel = CreateStackPanel(obj, propertiesToBeVisualized);
        }

        private StackPanel CreateStackPanel(dynamic obj, List<string> propertiesToBeVisualized)
        {
            StackPanel panel = new StackPanel();
            propertiesToBeVisualized
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