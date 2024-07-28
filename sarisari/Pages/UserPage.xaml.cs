using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Windows.Controls;
using static sarisari.AppExtension;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : WpfCore.BasePage<UserPageViewModel>, ICPassword
    {
        public SecureString SecurePassword => pin.SecurePassword;
        public SecureString SecureCPassword => cPin.SecurePassword;
        User user;
        public UserPage()
        {
            InitializeComponent();
        }
        public UserPage(UserPageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        //private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (!((sender as ListView).SelectedItem is User currentUser)) return;
        //    if (currentUser != user)
        //    {
        //        user = currentUser;
        //        foreach (var item in appViewModel.Users)
        //        {
        //            item.IsUpdating = item == user && item.IsUpdating;
        //        }
        //        var users = appViewModel.Users;
        //        appViewModel.Users = new ObservableCollection<User>(users);
        //    }
        //}

        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selected = (sender as ComboBox).SelectedItem as Role;
        //}
    }
}
