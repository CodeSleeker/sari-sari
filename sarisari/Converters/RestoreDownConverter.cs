using System;
using System.Globalization;

namespace sarisari
{
    public class RestoreDownConverter : WpfCore.BaseConverter<RestoreDownConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value - 2;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
