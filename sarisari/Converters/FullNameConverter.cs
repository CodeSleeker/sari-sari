using System;
using System.Globalization;

namespace sarisari
{
    public class FullNameConverter : WpfCore.BaseConverter<FullNameConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (User)value;
            return $"{user.FirstName} {user.LastName}";
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
