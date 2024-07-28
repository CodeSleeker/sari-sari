using System;
using System.Globalization;

namespace sarisari
{
    public class RoundedDecimalConverter:WpfCore.BaseConverter<RoundedDecimalConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Round((double)value, 2, MidpointRounding.AwayFromZero).ToString("0.00");
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
