using System;
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
            (obj.GetType() as Type).GetProperties().ToList()
                .ForEach(property =>
                    {
                        Label label = new Label();
                        label.Content = property.Name + ":" + property.GetValue(obj).ToString();

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