using System.Windows;
using System.Windows.Controls;

namespace sarisari
{
    public class ClearSelectionAttachedProperty : WpfCore.FrameworkAttachedProperty<ClearSelectionAttachedProperty>
    {
        protected override void DoAction(FrameworkElement element, bool value)
        {
            if (value)
            {
                if (element is ListView listView) listView.SelectedIndex = -1;
                if (element is ComboBox comboBox) comboBox.SelectedIndex = -1;
            }
        }
    }
}
