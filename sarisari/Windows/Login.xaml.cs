using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window/*, IPassword*/
    {
        //public SecureString SecurePassword => password.SecurePassword;
        public Login()
        {
            InitializeComponent();
            Topmost = true;
            this.DataContext = new LoginViewModel(this);
        }
    }
}
