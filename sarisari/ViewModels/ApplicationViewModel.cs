using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace sarisari
{
    public class ApplicationViewModel : WpfCore.BaseViewModel
    {
        private string _ProductCode;

        public string ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; OnPropertyChanged(); }
        }

        private string _Quantity;

        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }


        public string DeviceUUID { get; set; }
        public bool IsValidation { get; set; }
        public List<PINButton> PINButtons { get; set; } = new List<PINButton> {
            new PINButton{Text= "1", Size= "40" },
            new PINButton{Text= "2", Size= "40" },
            new PINButton{Text= "3", Size= "40" },
            new PINButton{Text= "4", Size= "40" },
            new PINButton{Text= "5", Size= "40" },
            new PINButton{Text= "6", Size= "40" },
            new PINButton{Text= "7", Size= "40" },
            new PINButton{Text= "8", Size= "40" },
            new PINButton{Text= "9", Size= "40" },
            new PINButton{Text= "CANCEL", Size= "20" },
            new PINButton{Text= "0", Size= "40" },
            new PINButton{Text= "CLEAR", Size= "20" },
        };
        public SecureString PIN { get; set; } = new SecureString();
        public string defaultImage = $@"{App.imgPath}\default.png";
        public bool IsRefreshed { get; set; }
        private User _User;
        public User User { get { return _User; } set { _User = value; OnPropertyChanged(); } }
        private bool _IsAdmin;
        public bool IsAdmin { get { return _IsAdmin; } set { _IsAdmin = value; OnPropertyChanged(); } }
        public bool HasChanged { get; set; }
        private ObservableCollection<SidePanel> _SidePanels = new ObservableCollection<SidePanel>
                {
                    new SidePanel{Name="Dashboard", Icon="ViewDashboard", IsAdmin=true,IsUser=true},
                    new SidePanel{Name="Cashier Out", Icon="CashRegister", IsAdmin=true,IsUser=true},
                    new SidePanel{Name="Change PIN", Icon = "FormTextboxPassword", IsUser=true, IsAdmin=false},
                    new SidePanel{
                        IsAdmin=true,
                        IsUser=false,
                        Name="Products",
                        Icon="BoxVariant",
                        RightIconVisibility=true,
                        SubPanel = new System.Collections.Generic.List<string>
                        {
                            "Product","Category","Size", "Supplier", "Stock"
                        }
                    },
                    new SidePanel{
                        IsAdmin=true,
                        IsUser=false,
                        Name="Users",
                        Icon="User",
                        RightIconVisibility=true,
                    SubPanel = new System.Collections.Generic.List<string>
                        {
                            "User","User Role"
                        }}
                };
        public ObservableCollection<SidePanel> SidePanels { get { return _SidePanels; } set { _SidePanels = value; OnPropertyChanged(); } }
        public WpfCore.BaseViewModel CurrentPageViewModel { get; set; }
        public ApplicationPage CurrentPage { get; set; }
        public void GotoPage(ApplicationPage page, WpfCore.BaseViewModel viewModel = null)
        {
            CurrentPageViewModel = viewModel;
            CurrentPage = page;
            OnPropertyChanged(nameof(CurrentPage));
        }
        public Window Window { get; set; }

        private string _Image;
        public string Image { get { return _Image; } set { _Image = value; OnPropertyChanged(); } }

        public ObservableCollection<Drawer> Drawers;

        private ObservableCollection<Product> _Products;
        public ObservableCollection<Product> Products { get { return _Products; } set { _Products = value; OnPropertyChanged(); } }

        private ObservableCollection<Category> _Categories;
        public ObservableCollection<Category> Categories { get { return _Categories; } set { _Categories = value; OnPropertyChanged(); } }

        private ObservableCollection<Role> _Roles;
        public ObservableCollection<Role> Roles { get { return _Roles; } set { _Roles = value; OnPropertyChanged(); } }

        private ObservableCollection<Size> _Sizes;
        public ObservableCollection<Size> Sizes { get { return _Sizes; } set { _Sizes = value; OnPropertyChanged(); } }

        private ObservableCollection<Stock> _Stocks;
        public ObservableCollection<Stock> Stocks { get { return _Stocks; } set { _Stocks = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier> _Suppliers;
        public ObservableCollection<Supplier> Suppliers { get { return _Suppliers; } set { _Suppliers = value; OnPropertyChanged(); } }

        private ObservableCollection<User> _Users = new ObservableCollection<User>();
        public ObservableCollection<User> Users { get { return _Users; } set { _Users = value; OnPropertyChanged(); } }

        private ObservableCollection<PurchaseProduct> _PurchasedProducts = new ObservableCollection<PurchaseProduct>();
        public ObservableCollection<PurchaseProduct> PurchasedProducts
        {
            get { return _PurchasedProducts; }
            set { _PurchasedProducts = value; OnPropertyChanged(); }
        }

        private List<Sales> _Sales = new List<Sales>();
        public List<Sales> Sales
        {
            get { return _Sales; }
            set { _Sales = value; OnPropertyChanged(); }
        }

        private Visibility _PurchasedProductsVisibility;
        public Visibility PurchasedProductsVisibility
        {
            get { return _PurchasedProductsVisibility; }
            set { _PurchasedProductsVisibility = value; OnPropertyChanged(); }
        }


        private bool _EditButtonVisibility = true;
        public bool EditButtonVisibility { get { return _EditButtonVisibility; } set { _EditButtonVisibility = value; OnPropertyChanged(); } }

        private bool _IsDialogOpen;
        public bool IsDialogOpen { get { return _IsDialogOpen; } set { _IsDialogOpen = value; OnPropertyChanged(); } }

        private string _DialogText;
        public string DialogText { get { return _DialogText; } set { _DialogText = value; OnPropertyChanged(); } }

        private string _FullName;
        public string FullName { get { return _FullName; } set { _FullName = value; OnPropertyChanged(); } }

        private string _SalesPrice;
        public string SalesPrice { get { return _SalesPrice; } set { _SalesPrice = value; OnPropertyChanged(); } }

        private string _TotalSalesPrice;
        public string TotalSalesPrice { get { return _TotalSalesPrice; } set { _TotalSalesPrice = value; OnPropertyChanged(); } }

        private Role _SelectedRole;
        public Role SelectedRole { get { return _SelectedRole; } set { _SelectedRole = value; OnPropertyChanged(); } }

        public bool Processing { get; set; }

        public bool IsPINWindowOpen { get; set; }

        public bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
    }
}
