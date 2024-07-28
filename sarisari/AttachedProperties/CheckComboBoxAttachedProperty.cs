using System.Windows;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    public class CheckComboBoxAttachedProperty : WpfCore.FrameworkAttachedProperty<CheckComboBoxAttachedProperty>
    {
        protected override void DoAction(FrameworkElement element, bool value)
        {
            if (value)
            {
                if(element is StackPanel sp)
                {
                    foreach (var item in sp.Children)
                    {
                        if(item is ComboBox cbo )
                        {
                            appViewModel.DialogText = cbo.Items.Count == 0?$"Please add {cbo.Tag}!":cbo.SelectedIndex == -1?$"Please choose a {cbo.Tag}":null;
                            if(appViewModel.DialogText != null)
                            {
                                appViewModel.IsDialogOpen = true;
                                appViewModel.Processing = false;
                                return;
                            }
                        }
                    }
                }
                appViewModel.Processing = false;
            }
        }
    }
}
