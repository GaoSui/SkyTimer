using SkyTimer.Utils.Decoder;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SkyTimer.Helper
{
    public class StackmatStatusToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (StackmatStatus)value;
            if (status == StackmatStatus.Green) return Brushes.LimeGreen;
            else if (status == StackmatStatus.Red) return Brushes.Red;
            else return Brushes.AliceBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
