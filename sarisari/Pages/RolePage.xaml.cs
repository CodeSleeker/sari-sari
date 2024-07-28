using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for RolePage.xaml
    /// </summary>
    public partial class RolePage : WpfCore.BasePage<RolePageViewModel>
    {
        public RolePage()
        {
            InitializeComponent();
        }
        public RolePage(RolePageViewModel viewModel):base(viewModel)
        {
            InitializeComponent();
        }

        //private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    appViewModel.EditButtonVisibility = true;

        //    foreach (var item in appViewModel.Roles)
        //    {
        //        item.IsUpdating = false;
        //    }
        //    var roles = appViewModel.Roles;
        //    appViewModel.Roles = new List<Role>(roles);
        //}
    }
}
