using System.Collections.Generic;
using System.Windows.Controls;

namespace ListProject.View.SingleObjectWindow
{
    public class SingleObjectPresenter
    {
        private StackPanel? _myStackPanel;

        public StackPanel? MyStackPanel
        {
            get => _myStackPanel;
            set => _myStackPanel = value;
        }

        public SingleObjectPresenter(dynamic obj, List<string> propertiesToBeVisualized)
        {
            MyStackPanel = new StackPanel();

            propertiesToBeVisualized
                .ForEach(property =>
                    {
                        Label label = new Label();
                        label.Content = property + ":" + obj.GetType().GetProperty(property).GetValue(obj);

                        StackPanel propertyStackPanel = new StackPanel();
                        propertyStackPanel.Orientation = Orientation.Horizontal;
                        propertyStackPanel.Children.Add(label);

                        MyStackPanel.Children.Add(propertyStackPanel);
                    }
                );
        }
    }
}