using System.Collections.ObjectModel;
using System.Windows.Controls;
using static sarisari.AppExtension;
namespace sarisari
{
    /// <summary>
    /// Interaction logic for StockPage.xaml
    /// </summary>
    public partial class StockPage : WpfCore.BasePage<StockPageViewModel>
    {
        Stock stock;
        public StockPage()
        {
            InitializeComponent();
        }
        public StockPage(StockPageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!((sender as ListView).SelectedItem is Stock currentStock)) return;
            if (currentStock != stock)
            {
                stock = currentStock;
                foreach (var item in appViewModel.Stocks)
                {
                    item.IsUpdating = item == stock && item.IsUpdating;
                }
                var stocks = appViewModel.Stocks;
                appViewModel.Stocks = new ObservableCollection<Stock>(stocks);
            }
        }
    }
}
