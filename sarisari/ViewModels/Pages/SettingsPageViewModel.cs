using System.Windows.Input;
using static sarisari.AppExtension;

namespace sarisari
{
    public class SettingsPageViewModel : WpfCore.BaseViewModel
    {
        public ICommand Supplier { get; set; }
        public ICommand Role { get; set; }
        public ICommand Category { get; set; }
        public ICommand Size { get; set; }
        public ICommand Stock { get; set; }

        public SettingsPageViewModel()
        {
            Supplier = new WpfCore.RelayCommand(() => appViewModel.GotoPage(ApplicationPage.SupplierPage));
            Role = new WpfCore.RelayCommand(() => appViewModel.GotoPage(ApplicationPage.RolePage));
            Category = new WpfCore.RelayCommand(() => appViewModel.GotoPage(ApplicationPage.CategoryPage));
            Size = new WpfCore.RelayCommand(() => appViewModel.GotoPage(ApplicationPage.SizePage));
            Stock = new WpfCore.RelayCommand(() => appViewModel.GotoPage(ApplicationPage.StockPage));
        }
    }
}
