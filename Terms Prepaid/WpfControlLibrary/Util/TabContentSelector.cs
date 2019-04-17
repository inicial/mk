using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfControlLibrary.ViewModel;

namespace WpfControlLibrary.Util
{
    /*
    public class StyleDictionary : Dictionary<String, Style> {}

    public class TabContentSelector2 : IValueConverter
    {
        public StyleDictionary StyleDictionary { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = Parser.GetInt(value);

            if (id != null)
            {
                Style style;
                if (StyleDictionary.TryGetValue(id.ToString(), out style))
                    return style;
            }

            throw new Exception(string.Format("Style not found id={0}", id));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    */

    public class DataTemplateDictionary : Dictionary<String, DataTemplate> { }

    public class TabContentSelector : DataTemplateSelector
    {
        public DataTemplateDictionary DataTemplateDictionary { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item is TabAbstractViewModel)
            {
                var vm = (TabAbstractViewModel) item;

                DataTemplate dataTemplate;
                if (DataTemplateDictionary.TryGetValue(vm.TypeId.ToString(), out dataTemplate))
                    return dataTemplate;
            }

            throw new Exception(string.Format("DataTemplate not found id={0}", item));
        }
    }
}
