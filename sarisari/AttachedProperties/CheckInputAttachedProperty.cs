using System.Windows;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    public class CheckInputAttachedProperty : WpfCore.FrameworkAttachedProperty<CheckInputAttachedProperty>
    {
        protected override void DoAction(FrameworkElement element, bool value)
        {
            if (value)
            {
                if (element is StackPanel sp)
                {
                    foreach (var item in sp.Children)
                    {
                        if ((item is TextBox tb && string.IsNullOrEmpty(tb.Text) && tb.Visibility != Visibility.Collapsed) || (item is PasswordBox pb && string.IsNullOrEmpty(pb.Password) && pb.Visibility != Visibility.Collapsed))
                        {
                            appViewModel.DialogText = "Please supply required fields!";
                            appViewModel.IsDialogOpen = true;
                            
                            appViewModel.Processing = false;
                            return;
                        }
                    }
                }
                appViewModel.Processing = false;
            }
        }
    }
}
