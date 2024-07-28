using System.Windows.Input;
using static sarisari.AppExtension;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Win32;
using System.IO;

namespace sarisari
{
    public class ProductPageViewModel : WpfCore.BaseViewModel
    {
        private bool _ClearSelection;
        public bool ClearSelection { get { return _ClearSelection; } set { _ClearSelection = value; OnPropertyChanged(); } }
        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _CheckComboBox;
        public bool CheckComboBox { get { return _CheckComboBox; } set { _CheckComboBox = value; OnPropertyChanged(); } }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private string _ImagePath;
        public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; OnPropertyChanged(); } }
        private string _ButtonText = "ADD";
        public string ButtonText { get { return _ButtonText; } set { _ButtonText = value; OnPropertyChanged(); } }
        private string _DialogTitle;

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value; OnPropertyChanged(); }
        }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand GetImage { get; set; }
        public ICommand AddProduct { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        private bool _IsUpdating = false;
        public bool IsUpdating { get { return _IsUpdating; } set { _IsUpdating = value; OnPropertyChanged(); } }
        private bool _IsAddProductDialogOpen;
        public bool IsAddProductDialogOpen { get { return _IsAddProductDialogOpen; } set { _IsAddProductDialogOpen = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Code;
        public string Code { get { return _Code; } set { _Code = value; OnPropertyChanged(); } }
        private string _Description;
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged(); } }
        private string _PurchasePrice;
        public string PurchasePrice
        {
            get { return _PurchasePrice; }
            set { _PurchasePrice = value; OnPropertyChanged(); }
        }
        private string _SalesPrice;
        public string SalesPrice
        {
            get { return _SalesPrice; }
            set { _SalesPrice = value; OnPropertyChanged(); }
        }

        private string _TotalUnits;
        public string TotalUnits { get { return _TotalUnits; } set { _TotalUnits = value; OnPropertyChanged(); } }
        public string Stocks { get; set; }
        private string _ItemsPerUnit;
        public string ItemsPerUnit { get { return _ItemsPerUnit; } set { _ItemsPerUnit = value; OnPropertyChanged(); } }
        private Size _SelectedSize;
        public Size SelectedSize { get { return _SelectedSize; } set { _SelectedSize = value; OnPropertyChanged(); } }
        private Category _SelectedCategory;
        public Category SelectedCategory { get { return _SelectedCategory; } set { _SelectedCategory = value; OnPropertyChanged(); } }
        private Supplier _SelectedSupplier;
        public Supplier SelectedSupplier { get { return _SelectedSupplier; } set { _SelectedSupplier = value; OnPropertyChanged(); } }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value; OnPropertyChanged(); } }
        Product product;
        Stock stock;
        public ProductPageViewModel()
        {
            appViewModel.Image = appViewModel.defaultImage;
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Products.Count > 0;
            //foreach (var item in appViewModel.Products)
            //{
            //    item.IsUpdating = false;
            //}
            //appViewModel.EditButtonVisibility = true;
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                product = param as Product;
                foreach (var item in appViewModel.Stocks)
                {
                    if (item.Product == product)
                    {
                        await ServiceExtension<Stock, DataDbContext>.Store.RemoveData(item);
                        appViewModel.Stocks.Remove(item);
                        break;
                    }
                }
                await ServiceExtension<Product, DataDbContext>.Store.RemoveData(product);
                appViewModel.Products.Remove(product);
                ListViewVisibility = appViewModel.Products.Count > 0;
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
                        int stocks;
                        if (ImagePath != null)
                        {
                            FileInfo fi = new FileInfo(appViewModel.Image);
                            await FileHelper.CopyFile(ImagePath, $@"{App.imgPath}\{Name}{fi.Extension}");
                            ImagePath = $@"{App.imgPath}\{Name}{fi.Extension}";
                        }
                        product = ButtonText == "ADD" ? new Product() : product;
                        product.Code = Code;
                        product.Name = Name;
                        product.Description = Description;
                        product.Category = SelectedCategory;
                        product.Size = SelectedSize;
                        product.Supplier = SelectedSupplier;
                        product.SalesPrice = Convert.ToDouble(SalesPrice);
                        product.Image = ImagePath;
                        try
                        {
                            stocks = ButtonText == "ADD" ?
                            Convert.ToInt32(ItemsPerUnit) * Convert.ToInt32(TotalUnits) :
                            stock.Quantity;
                        }
                        catch
                        {
                            appViewModel.DialogText = "Stocks must be numeric";
                            appViewModel.IsDialogOpen = true;
                            return;
                        }
                        product.ItemPurchasePrice = ButtonText == "ADD" ? Convert.ToDouble(PurchasePrice) / Convert.ToInt32(ItemsPerUnit) : product.ItemPurchasePrice;
                        if (ButtonText == "ADD")
                        {
                            product.DateAdded = DateTime.Now;
                            product.DateModified = DateTime.Now;
                            await ServiceExtension<Product, DataDbContext>.Store.AddData(product);
                            stock = new Stock
                            {
                                Product = product,
                                Quantity = stocks,
                                DateAdded = DateTime.Now,
                                DateModified = DateTime.Now
                            };
                            await ServiceExtension<Stock, DataDbContext>.Store.AddData(stock);
                            appViewModel.Products.Add(product);
                            appViewModel.Stocks.Add(stock);
                        }
                        else
                        {
                            product.DateModified = DateTime.Now;
                            await ServiceExtension<Product, DataDbContext>.Store.UpdateData(product);

                            stock.Quantity = stocks;
                            await ServiceExtension<Stock, DataDbContext>.Store.UpdateData(stock);
                        }
                        ListViewVisibility = true;
                        IsAddProductDialogOpen = false;
                        IsPageEnabled = true;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                product = param as Product;
                stock = appViewModel.Stocks.Where(stock => stock.Product == product).FirstOrDefault();
                Name = product.Name;
                Code = product.Code;
                Description = product.Description;
                SelectedCategory = product.Category;
                SelectedSupplier = product.Supplier;
                SelectedSize = product.Size;
                SalesPrice = product.SalesPrice.ToString("0.00");
                PurchasePrice = product.ItemPurchasePrice.ToString("0.00");
                ButtonText = "UPDATE";
                IsUpdating = true;
                IsAddProductDialogOpen = true;
                IsPageEnabled = false;
            });
            GetImage = new WpfCore.RelayCommand(() =>
            {
                ClearSelection = true;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                var result = openFileDialog.ShowDialog();
                if ((bool)result)
                {
                    ImagePath = openFileDialog.FileName;
                    appViewModel.Image = ImagePath;
                }
            });
            AddProduct = new WpfCore.RelayCommand(() =>
            {
                ClearInput = true;
                ButtonText = "ADD";
                DialogTitle = "ADD PRODUCT";
                if (appViewModel.Categories.Count == 0)
                {
                    appViewModel.DialogText = "Please add a category first before adding a product.";
                    appViewModel.IsDialogOpen = true;

                }
                else if (appViewModel.Sizes.Count == 0)
                {
                    appViewModel.DialogText = "Please define a size first before adding a product.";
                    appViewModel.IsDialogOpen = true;
                }
                else if (appViewModel.Suppliers.Count == 0)
                {
                    appViewModel.DialogText = "Please add a supplier first before adding a product.";
                    appViewModel.IsDialogOpen = true;
                }
                else
                    IsAddProductDialogOpen = true;
                IsPageEnabled = false;
            });
            Cancel = new WpfCore.RelayCommand(() =>
           {
               appViewModel.Image = appViewModel.defaultImage;
               IsAddProductDialogOpen = false;
               IsPageEnabled = true;
           });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddProductDialogOpen;
            });
        }
    }
}
