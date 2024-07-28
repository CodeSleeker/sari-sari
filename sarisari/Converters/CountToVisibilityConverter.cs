﻿using System;
using System.Globalization;
using System.Windows;

namespace sarisari
{
    public class CountToVisibilityConverter : WpfCore.BaseConverter<CountToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0) return Visibility.Visible;
            else return Visibility.Hidden;
        }
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
