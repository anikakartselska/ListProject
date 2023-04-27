using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace ListProject.View.SingleObjectWindow
{
    public class SingleObjectPresenter<T>
    {
        private StackPanel _stackPanel;

        public StackPanel StackPanel
        {
            get => _stackPanel;
            set => _stackPanel = value;
        }

        public SingleObjectPresenter(T obj,bool testMode)
        {
            StackPanel = new StackPanel();

            // Get the list of properties for the input object
            List<PropertyInfo> properties = typeof(T).GetProperties()
                .OrderByDescending(x => x.Name.Equals("Id",StringComparison.OrdinalIgnoreCase)).ToList();
            // Create a label and a text box for each property, and add them to the stack panel
            foreach (PropertyInfo property in properties)
            {
                if (!testMode && property.Name.Equals("Id", StringComparison.CurrentCultureIgnoreCase)) continue;
                Label label = new Label();
                label.Content = property.Name + ":" +  property.GetValue(obj);

                // Label textBox = new TextBox();
                // textBox.Text = property.GetValue(obj).ToString();

                StackPanel propertyStackPanel = new StackPanel();
                propertyStackPanel.Orientation = Orientation.Horizontal;
                propertyStackPanel.Children.Add(label);
                // propertyStackPanel.Children.Add(textBox);

                StackPanel.Children.Add(propertyStackPanel);
            }
        }
    }
}