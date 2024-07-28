using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace sarisari
{
    public class UsersToCashierConverter : WpfCore.BaseConverter<UsersToCashierConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var users = (ObservableCollection<User>)value;
            return users.Where(x => x.Role.Name.ToLower() == "cashier");
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
