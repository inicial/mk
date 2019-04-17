using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfControlLibrary.Util
{
    public class BrightConverter : IValueConverter
    {
        public byte Deltha { get; set; }

        public BrightConverter(byte deltha)
        {
            Deltha = deltha;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var brush = (SolidColorBrush) value;
            
            const byte deltha = 45;

            brush.Color = new Color
            {
                R = (byte)(brush.Color.R + Deltha),
                G = (byte)(brush.Color.G + Deltha),
                B = (byte)(brush.Color.B + Deltha)
            };

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
