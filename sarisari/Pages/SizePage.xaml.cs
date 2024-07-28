using System.Collections.ObjectModel;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for SizePage.xaml
    /// </summary>
    public partial class SizePage : WpfCore.BasePage<SizePageViewModel>
    {
        Size size;
        public SizePage()
        {
            InitializeComponent();
        }
        public SizePage(SizePageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (!((sender as ListView).SelectedItem is Size currentSize)) return;
            if (currentSize != size)
            {
                size = currentSize;
                foreach (var item in appViewModel.Sizes)
                {
                    item.IsUpdating = item == size && item.IsUpdating;
                }
                var sizes = appViewModel.Sizes;
                appViewModel.Sizes = new ObservableCollection<Size>(sizes);
            }
        }
    }
}
