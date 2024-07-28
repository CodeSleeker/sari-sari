using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class ProductPage : WpfCore.BasePage<ProductPageViewModel>
    {
        Product product;
        public ProductPage()
        {
            InitializeComponent();
        }
        public ProductPage(ProductPageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(purchasePrice.Text))
                {
                    appViewModel.SalesPrice = null;
                    appViewModel.TotalSalesPrice = null;
                    return;
                }
                if (string.IsNullOrEmpty(itemsPerUnit.Text))
                {
                    appViewModel.SalesPrice = null;
                    appViewModel.TotalSalesPrice = null;
                    return;
                }
                double pPrice = Convert.ToDouble(purchasePrice.Text);
                double totalItems = Convert.ToDouble(itemsPerUnit.Text);
                double sPrice = 0.0;
                double iPurchasePrice = pPrice / totalItems;
                if (iPurchasePrice <= 10) sPrice = Math.Round(iPurchasePrice + 1);
                else if (iPurchasePrice > 10 && iPurchasePrice <= 20) sPrice = Math.Round(iPurchasePrice + 2);
                else sPrice = Math.Round(iPurchasePrice * 1.2);
                salesPrice.Text = sPrice.ToString("0.00");
            }
            catch {
                appViewModel.SalesPrice = null;
                appViewModel.TotalSalesPrice = null;
            }
        }

        private void totalItems_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(purchasePrice.Text))
                {
                    appViewModel.SalesPrice = null;
                    appViewModel.TotalSalesPrice = null;
                    return;
                }
                if (string.IsNullOrEmpty(itemsPerUnit.Text))
                {
                    appViewModel.SalesPrice = null;
                    appViewModel.TotalSalesPrice = null;
                    return;
                }
                double pPrice = Convert.ToDouble(purchasePrice.Text);
                double totalItems = Convert.ToDouble(itemsPerUnit.Text);
                double sPrice = 0.0;
                double iPurchasePrice = pPrice / totalItems;
                if (iPurchasePrice <= 10) sPrice = Math.Round(iPurchasePrice + 1);
                else if (iPurchasePrice > 10 && iPurchasePrice <= 20) sPrice = Math.Round(iPurchasePrice + 2);
                else sPrice = Math.Round(iPurchasePrice * 1.2);
                salesPrice.Text = sPrice.ToString("0.00");
            }
            catch
            {
                appViewModel.SalesPrice = null;
                appViewModel.TotalSalesPrice = null;
            }
        }
    }
}
