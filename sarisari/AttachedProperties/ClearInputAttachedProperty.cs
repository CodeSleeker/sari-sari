using System.Windows;
using System.Windows.Controls;

namespace sarisari
{
    public class ClearInputAttachedProperty : WpfCore.FrameworkAttachedProperty<ClearInputAttachedProperty>
    {
        protected override void DoAction(FrameworkElement element, bool value)
        {
            if (value)
            {
                if (element is StackPanel sp)
                {
                    foreach (var item in sp.Children)
                    {
                        if (item is TextBox textBox) textBox.Text = "";
                        if (item is PasswordBox passwordBox) passwordBox.Password = "";
                        if (item is Image image) image.Source = null;
                        if (item is ComboBox comboBox) comboBox.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}
