using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static sarisari.AppExtension;

namespace sarisari
{
    public class PINEnteredAttachedProperty : WpfCore.FrameworkAttachedProperty<PINEnteredAttachedProperty>
    {
        protected override void DoAction(FrameworkElement element, bool value)
        {
            if(element is StackPanel sp)
            {
                for (int i = 0; i < appViewModel.PIN.Length; i++)
                {
                    (sp.Children[i] as TextBlock).Foreground = new SolidColorBrush(Colors.Black);
                }
                if(appViewModel.PIN.Length == 0)
                {
                    foreach (var item in sp.Children)
                    {
                        (item as TextBlock).Foreground = new SolidColorBrush(Colors.Gray);
                    }
                }
            }
        }
    }
}
