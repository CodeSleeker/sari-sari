using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static sarisari.AppExtension;

namespace sarisari
{
    public class StockPageViewModel : WpfCore.BaseViewModel
    {
        private Product _SelectedProduct;

        public Product SelectedProduct
        {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value;OnPropertyChanged(); }
        }

        private string _Quantity;

        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value;OnPropertyChanged(); }
        }

        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        private bool _IsAddStockDialogOpen;

        public bool IsAddStockDialogOpen
        {
            get { return _IsAddStockDialogOpen; }
            set { _IsAddStockDialogOpen = value;OnPropertyChanged(); }
        }

        private string _DialogTitle;

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value; OnPropertyChanged(); }
        }
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set { _ButtonText = value; OnPropertyChanged(); }
        }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value;OnPropertyChanged(); } }
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _CheckComboBox;
        public bool CheckComboBox { get { return _CheckComboBox; } set { _CheckComboBox = value; OnPropertyChanged(); } }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private string _Product;

        public string Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged(); }
        }

        Stock stock;
        public StockPageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Stocks.Count > 0;
            //foreach (var item in appViewModel.Stocks)
            //{
            //    item.IsUpdating = false;
            //}
            //appViewModel.EditButtonVisibility = true;
            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await StockStore.UpdateStockAsync(param as Stock);
            //    foreach (var item in appViewModel.Stocks)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var stocks = appViewModel.Stocks;
            //    appViewModel.Stocks = new ObservableCollection<Stock>(stocks);
            //    appViewModel.EditButtonVisibility = true;
            //});
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                stock = param as Stock;
                if (appViewModel.Products.Where(product => stock.Product == product).Any())
                {
                    appViewModel.DialogText = "Cannot delete stocks. Product exist.";
                    appViewModel.IsDialogOpen = true;
                    IsPageEnabled = false;
                    return;
                }
                foreach (var item in appViewModel.Stocks)
                {
                    if (item == stock)
                    {
                        await ServiceExtension<Stock, DataDbContext>.Store.RemoveData(item);
                        appViewModel.Stocks.Remove(item);
                        ListViewVisibility = appViewModel.Stocks.Count > 0;
                        break;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                stock = param as Stock;
                Product = stock.Product.Name;
                Quantity = stock.Quantity.ToString();
                ButtonText = "UPDATE";
                DialogTitle = "UPDATE STOCK";
                IsAddStockDialogOpen = true;
                IsPageEnabled = false;
            });
            Add = new WpfCore.RelayParameterCommand(async (param) =>
            {
                appViewModel.Processing = true;
                CheckInput = true;
                await Task.Run(async () =>
                {
                    while (appViewModel.Processing) await Task.Delay(1000);
                });
                if (!appViewModel.IsDialogOpen)
                {
                    appViewModel.Processing = true;
                    CheckComboBox = true;
                    await Task.Run(async () =>
                    {
                        while (appViewModel.Processing) await Task.Delay(1000);
                    });
                    if (!appViewModel.IsDialogOpen)
                    {
                        if (ButtonText == "ADD")
                        {
                            stock = new Stock
                            {
                                Product = SelectedProduct,
                                Quantity = Convert.ToInt32(Quantity),
                                DateAdded = DateTime.Now,
                                DateModified = DateTime.Now
                            };
                            await ServiceExtension<Stock, DataDbContext>.Store.AddData(stock);
                            appViewModel.Stocks.Add(stock);
                        }
                        else
                        {
                            stock.Product = SelectedProduct;
                            stock.Quantity = Convert.ToInt32(Quantity);
                            stock.DateModified = DateTime.Now;
                            await ServiceExtension<Stock, DataDbContext>.Store.UpdateData(stock);
                        }
                        ListViewVisibility = true;
                        IsAddStockDialogOpen = false;
                        IsPageEnabled = true;
                    }
                }
            });
            Cancel = new WpfCore.RelayCommand(() =>
            {
                IsAddStockDialogOpen = false;
                IsPageEnabled = true;
            });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddStockDialogOpen;
            });
        }
    }
}
