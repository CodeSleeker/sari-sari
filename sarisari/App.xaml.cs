using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string path = $@"{appdata}\SariSari";
        public static string imgPath = $@"{path}\Assets\Images";
        protected async override void OnStartup(StartupEventArgs e)
        {
            appViewModel.DeviceUUID = DeviceHelper.GetCSProduct()["UUID"];
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("sarisari.Assets.Images.luffy.png");
            if (!Directory.Exists(imgPath)) Directory.CreateDirectory(imgPath);
            if (stream != null)
            {
                using (Stream input = stream)
                {
                    using (Stream output = File.Create($@"{imgPath}\default.png"))
                    {
                        input.CopyTo(output);
                    }
                }
            }
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            Constants.dbLocation = $@"{path}\SariSari.db";
            await InitializeData();
            new Login().Show();
            //new MainWindow().Show();
            //appViewModel.GotoPage(ApplicationPage.UserPage);
            base.OnStartup(e);


        }
        private async Task InitializeData()
        {
            try
            {
                var drawers = await ServiceExtension<Drawer, DataDbContext>.Store.GetAllData();
                drawers.ForEach(async x =>
                {
                    if (x.CashierId != null)
                        x.Cashier = await ServiceExtension<User, DataDbContext>.Store.GetData((int)x.CashierId);
                });
                appViewModel.Drawers = new ObservableCollection<Drawer>(drawers);
                var products = await ServiceExtension<Product, DataDbContext>.Store.GetAllData();
                products.ForEach(async x =>
                {
                    x.Size = await ServiceExtension<Size, DataDbContext>.Store.GetData((int)x.SizeId);
                    x.Supplier = await ServiceExtension<Supplier, DataDbContext>.Store.GetData((int)x.SupplierId);
                    x.Category = await ServiceExtension<Category, DataDbContext>.Store.GetData((int)x.CategoryId);
                });
                appViewModel.Products = new ObservableCollection<Product>(products);
                var categories = await ServiceExtension<Category, DataDbContext>.Store.GetAllData();
                appViewModel.Categories = new ObservableCollection<Category>(categories);
                var roles = await ServiceExtension<Role, DataDbContext>.Store.GetAllData();
                appViewModel.Roles = new ObservableCollection<Role>(roles);
                var sizes = await ServiceExtension<Size, DataDbContext>.Store.GetAllData();
                appViewModel.Sizes = new ObservableCollection<Size>(sizes);
                var stocks = await ServiceExtension<Stock, DataDbContext>.Store.GetAllData();
                stocks.ForEach(async x =>
                {
                    x.Product = await ServiceExtension<Product, DataDbContext>.Store.GetData((int)x.ProductId);
                });
                appViewModel.Stocks = new ObservableCollection<Stock>(stocks);
                var suppliers = await ServiceExtension<Supplier, DataDbContext>.Store.GetAllData();
                appViewModel.Suppliers = new ObservableCollection<Supplier>(suppliers);
                var users = await ServiceExtension<User, DataDbContext>.Store.GetAllData();
                users.ForEach(async x => x.Role = await ServiceExtension<Role, DataDbContext>.Store.GetData((int)x.RoleId));
                appViewModel.Users = new ObservableCollection<User>(users);
            }
            catch
            {
                File.Delete($@"{path}\SariSari.db");
                await InitializeData();
            }
        }
    }
}
