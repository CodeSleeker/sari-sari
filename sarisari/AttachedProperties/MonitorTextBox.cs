using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static sarisari.AppExtension;
namespace sarisari
{
    public class MonitorProductCodeTextBox : WpfCore.BaseAttachedProperty<MonitorProductCodeTextBox, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is TextBox textbox))
                return;
            textbox.TextChanged -= Textbox_TextChanged;
            if ((bool)e.NewValue)
            {
                textbox.TextChanged += Textbox_TextChanged;
            }
        }
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            appViewModel.Products.ToList().ForEach(x =>
            {
                if (x.Code == textbox.Text)
                {
                    var product = appViewModel.PurchasedProducts.Where(p => p.Code == x.Code).FirstOrDefault();
                    if (product == null)
                    {
                        appViewModel.PurchasedProducts.Add(new PurchaseProduct
                        {
                            Code = x.Code,
                            Name = x.Name,
                            UnitPrice = x.SalesPrice,
                            Discount = 0,
                            Quantity = appViewModel.Quantity == null ? 1 : Convert.ToInt32(appViewModel.Quantity),
                            Total = x.SalesPrice
                        });
                        appViewModel.PurchasedProductsVisibility = Visibility.Visible;
                    }
                    else
                    {
                        product.Quantity = appViewModel.Quantity == null ? product.Quantity + 1 : Convert.ToInt32(appViewModel.Quantity);
                        product.Total = product.UnitPrice * product.Quantity;
                    }
                    textbox.Text = null;
                    appViewModel.Quantity = null;
                }
            });
        }
    }
}
