using System.Collections.ObjectModel;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : WpfCore.BasePage<CategoryPageViewModel>
    {
        public CategoryPage()
        {
            InitializeComponent();
        }
        public CategoryPage(CategoryPageViewModel viewModel):base(viewModel)
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appViewModel.EditButtonVisibility = true;

            foreach (var item in appViewModel.Categories    )
            {
                item.IsUpdating = false;
            }
            var categories = appViewModel.Categories;
            appViewModel.Categories = new ObservableCollection<Category>(categories);
        }

        private void ScrollViewer_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
