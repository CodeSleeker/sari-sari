using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static sarisari.AppExtension;
namespace sarisari
{
    public class MainPageViewModel : WpfCore.BaseViewModel
    {
        private string _Cashier;

        public string Cashier
        {
            get { return _Cashier; }
            set { _Cashier = value; OnPropertyChanged(); }
        }


        private Visibility _AssignDrawerButtonVisibility;

        public Visibility AssignDrawerButtonVisibility
        {
            get { return _AssignDrawerButtonVisibility; }
            set { _AssignDrawerButtonVisibility = value; OnPropertyChanged(); }
        }
        private int _DrawerID;

        public int DrawerID
        {
            get { return _DrawerID; }
            set { _DrawerID = value; OnPropertyChanged(); }
        }

        public ICommand AssignCommand { get; set; }
        public ICommand Assign { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        public string DeviceName { get; set; }
        private bool _AssignDrawer;
        public bool AssignDrawer { get { return _AssignDrawer; } set { _AssignDrawer = value; OnPropertyChanged(); } }
        private User _SelectedUser;
        public User SelectedUser { get { return _SelectedUser; } set { _SelectedUser = value;OnPropertyChanged(); } }
        Drawer drawer;
        

        public MainPageViewModel()
        {
            appViewModel.PurchasedProductsVisibility = appViewModel.PurchasedProducts.Count>0? Visibility.Visible:Visibility.Hidden;
            CommandDialog = new WpfCore.RelayCommand(() => appViewModel.IsDialogOpen = false);
            AssignCommand = new WpfCore.RelayCommand(() =>
            {
                if (appViewModel.Users.Where(x => x.Role.Name.ToLower() == "cashier").Any())
                    AssignDrawer = true;
                else
                {
                    appViewModel.DialogText = "Add a cashier first";
                    appViewModel.IsDialogOpen = true;

                }
            });
            Assign = new WpfCore.RelayCommand(() =>
            {
                drawer.Cashier = SelectedUser;
                Task.FromResult(ServiceExtension<Drawer, DataDbContext>.Store.UpdateData(drawer));
                Cashier = $"{drawer.Cashier?.FirstName.ToUpper()} {drawer.Cashier?.LastName.ToUpper()}";
                AssignDrawer = false;
            });
            Cancel = new WpfCore.RelayCommand(() => AssignDrawer = false);

            drawer = appViewModel.Drawers.ToList().Where(x => x.DeviceUUID == appViewModel.DeviceUUID).FirstOrDefault();
            if (drawer == null)
            {
                AssignDrawerButtonVisibility = appViewModel.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
                drawer = new Drawer
                {
                    DeviceUUID = appViewModel.DeviceUUID,
                    DrawerId = appViewModel.Drawers.Count() + 1,
                    Cashier = !appViewModel.IsAdmin ? appViewModel.User : null
                };
                appViewModel.Drawers.Add(drawer);
                Task.Run(async () =>
                {
                    await ServiceExtension<Drawer, DataDbContext>.Store.AddData(drawer);
                });
            }
            else
            {
                
                AssignDrawerButtonVisibility = drawer.Cashier == null || appViewModel.IsAdmin ? Visibility.Visible : Visibility.Collapsed;
                if (!appViewModel.IsAdmin && drawer.Cashier == null)
                {
                    drawer.Cashier = appViewModel.User;
                    ServiceExtension<Drawer, DataDbContext>.Store.UpdateData(drawer).GetAwaiter().GetResult();
                }
            }
            DrawerID = drawer.DrawerId;
            Cashier = $"{drawer.Cashier?.FirstName.ToUpper()} {drawer.Cashier?.LastName.ToUpper()}";
            DeviceName = Environment.MachineName;
        }
    }
}
