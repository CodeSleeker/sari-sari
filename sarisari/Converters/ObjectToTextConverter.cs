using System;
using System.Globalization;

namespace sarisari
{
    public class ObjectToTextConverter : WpfCore.BaseConverter<ObjectToTextConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var role = value as Role;
            return role==null?"":role.Name;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
