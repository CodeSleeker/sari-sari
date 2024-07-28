using System.Collections.ObjectModel;
using static sarisari.AppExtension;
namespace sarisari
{
    /// <summary>
    /// Interaction logic for SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : WpfCore.BasePage<SupplierPageViewModel>
    {
        public SupplierPage()
        {
            InitializeComponent();
        }
        public SupplierPage(SupplierPageViewModel viewModel):base(viewModel)
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            appViewModel.EditButtonVisibility = true;

            foreach (var item in appViewModel.Suppliers)
            {
                item.IsUpdating = false;
            }
            var suppliers = appViewModel.Suppliers;
            appViewModel.Suppliers = new ObservableCollection<Supplier>(suppliers);
        }
    }
}
