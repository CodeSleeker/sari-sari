using System.Threading.Tasks;
using System.Windows.Input;
using static sarisari.AppExtension;
using System.Collections.ObjectModel;
using System.Linq;

namespace sarisari
{
    public class SupplierPageViewModel : WpfCore.BaseViewModel
    {
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Address;
        public string Address { get { return _Address; } set { _Address = value; OnPropertyChanged(); } }
        private string _PhoneNumber;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value;OnPropertyChanged(); }
        }

        private string _MobileNumber;

        public string MobileNumber
        {
            get { return _MobileNumber; }
            set { _MobileNumber = value;OnPropertyChanged(); }
        }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand AddSupplier { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set { _ButtonText = value;OnPropertyChanged(); }
        }
        private string _DialogTitle;

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value;OnPropertyChanged(); }
        }
        private bool _IsAddSupplierDialogOpen;

        public bool IsAddSupplierDialogOpen
        {
            get { return _IsAddSupplierDialogOpen; }
            set { _IsAddSupplierDialogOpen = value;OnPropertyChanged(); }
        }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value; OnPropertyChanged(); } }
        Supplier supplier;
        public SupplierPageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Suppliers.Count > 0 ? true : false;
            appViewModel.EditButtonVisibility = true;
            //foreach (var item in appViewModel.Suppliers)
            //{
            //    item.IsUpdating = false;
            //}
            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await SupplierStore.UpdateSupplierAsync(param as Supplier);
            //    foreach (var item in appViewModel.Suppliers)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var suppliers = appViewModel.Suppliers;
            //    appViewModel.Suppliers = new ObservableCollection<Supplier>(suppliers);
            //    appViewModel.EditButtonVisibility = true;
            //});
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                supplier = param as Supplier;
                if (appViewModel.Products.Where(x => x.Supplier == supplier).Any())
                {
                    appViewModel.DialogText = "This supplier is in used!";
                    appViewModel.IsDialogOpen = true;
                    return;
                }
                foreach (var item in appViewModel.Suppliers)
                {
                    if (item == supplier)
                    {
                        appViewModel.Suppliers.Remove(item);
                        await ServiceExtension<Supplier, DataDbContext>.Store.RemoveData(item);
                        ListViewVisibility = appViewModel.Suppliers.Count > 0 ? true : false;
                        return;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                supplier = param as Supplier;
                Name = supplier.Name;
                Address = supplier.Address;
                PhoneNumber = supplier.PhoneNumber;
                MobileNumber = supplier.MobileNumber;
                ButtonText = "UPDATE";
                DialogTitle = "UPDATE SUPPLIER";
                IsAddSupplierDialogOpen = true;
                IsPageEnabled = false;
            });
            Add = new WpfCore.RelayCommand(async () =>
             {
                 CheckInput = true;
                 await Task.Run(async () =>
                 {
                     while (appViewModel.Processing) { await Task.Delay(1000); }
                 });
                 if (!appViewModel.IsDialogOpen)
                 {
                     if (ButtonText == "ADD")
                     {
                         supplier = new Supplier
                         {
                             Name = Name,
                             Address = Address,
                             PhoneNumber = PhoneNumber,
                             MobileNumber = MobileNumber
                         };
                         appViewModel.Suppliers.Add(supplier);
                         await ServiceExtension<Supplier, DataDbContext>.Store.AddData(supplier);
                     }
                     else
                     {
                         supplier.Name = Name;
                         supplier.Address = Address;
                         supplier.PhoneNumber = PhoneNumber;
                         supplier.MobileNumber = MobileNumber;
                         await ServiceExtension<Supplier, DataDbContext>.Store.UpdateData(supplier);
                     }
                     IsAddSupplierDialogOpen = false;
                     ListViewVisibility = true;
                     IsPageEnabled = true;
                 }
             });
            AddSupplier = new WpfCore.RelayCommand(() =>
            {
                ClearInput = true;
                IsAddSupplierDialogOpen = true;
                ButtonText = "ADD";
                DialogTitle = "ADD SUPPLIER";
                IsPageEnabled = false;
            });
            Cancel = new WpfCore.RelayCommand(() =>
            {
                IsAddSupplierDialogOpen = false;
                IsPageEnabled = true;
            });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddSupplierDialogOpen;
            });
        }
    }
}
