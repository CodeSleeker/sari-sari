using System;
using System.Globalization;
using System.Windows;
using WpfCore;
using static sarisari.AppExtension;
namespace sarisari
{
    public class SidePanelvisibilityConverter : BaseConverter<SidePanelvisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var obj = (SidePanel)value;
            if (appViewModel.IsAdmin)
                return obj.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
            else
                return obj.IsUser ? Visibility.Visible : Visibility.Collapsed;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
