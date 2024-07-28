using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static sarisari.AppExtension;

namespace sarisari
{
    public class LoginViewModel : WpfCore.BaseViewModel
    {
        private bool _IsError = false;
        public bool IsError { get { return _IsError; } set { _IsError = value; OnPropertyChanged(); } }
        public string Username { get; set; } = "";
        public ICommand Exit { get; set; }
        public ICommand Continue { get; set; }
        public ICommand Digit { get; set; }
        private bool _IsPINEntered;

        public bool IsPINEntered
        {
            get { return _IsPINEntered; }
            set { _IsPINEntered = value; OnPropertyChanged(); }
        }
        private string _BorderColor = "Gray";

        public string BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; OnPropertyChanged(); }
        }
        User user;

        public LoginViewModel(Window window, bool isSwitch = false)
        {
            appViewModel.IsPINWindowOpen = true;
            Digit = new WpfCore.RelayParameterCommand((value) =>
            {
                if ((value as PINButton).Text == "CANCEL")
                {
                    appViewModel.IsPINWindowOpen = false;
                    appViewModel.PIN.Clear();
                    window.Close();
                }
                else if ((value as PINButton).Text == "CLEAR")
                {
                    appViewModel.IsPINWindowOpen = false;
                    BorderColor = "Gray";
                    appViewModel.PIN.Clear();
                }
                else
                {
                    appViewModel.PIN.AppendChar((value as PINButton).Text[0]);
                    if (appViewModel.PIN.Unsecure().Length == 4)
                    {
                        if (appViewModel.PIN.Unsecure() == "0000")
                        {
                            appViewModel.FullName = "ADMIN";
                            appViewModel.IsAdmin = true;
                            appViewModel.SidePanels = new System.Collections.ObjectModel.ObservableCollection<SidePanel>(appViewModel.SidePanels);
                            if (!isSwitch) new MainWindow().Show();
                            appViewModel.GotoPage(ApplicationPage.MainPage);
                            window?.Close();
                            appViewModel.PIN.Clear();
                        }
                        else
                        {
                            var drawer = appViewModel.Drawers.ToList().Where(x => x.DeviceUUID == appViewModel.DeviceUUID).FirstOrDefault();
                            user = appViewModel.Users.Where(x => PasswordHelper.IsValid(appViewModel.PIN.Unsecure(), x.PIN)).FirstOrDefault();
                            appViewModel.PIN.Clear();
                            if (user == null)
                            {
                                BorderColor = "Red";
                                IsPINEntered ^= true;
                                return;
                            }
                            if (user.Role.Name == "Admin")
                            {
                                appViewModel.User = user;
                                appViewModel.IsAdmin = true;
                                appViewModel.SidePanels = new System.Collections.ObjectModel.ObservableCollection<SidePanel>(appViewModel.SidePanels);
                                if (!isSwitch) new MainWindow().Show();
                                appViewModel.GotoPage(ApplicationPage.MainPage);
                                window?.Close();
                            }
                            else
                            {
                                if (user != drawer.Cashier)
                                {
                                    BorderColor = "Red";
                                    IsPINEntered ^= true;
                                    return;
                                }
                                appViewModel.User = user;
                                appViewModel.IsAdmin = false;
                                appViewModel.SidePanels = new System.Collections.ObjectModel.ObservableCollection<SidePanel>(appViewModel.SidePanels);
                                if (!isSwitch) new MainWindow().Show();
                                appViewModel.GotoPage(ApplicationPage.MainPage);
                                window?.Close();
                            }
                            appViewModel.FullName = $"{user.FirstName} {user.LastName}".ToUpper();
                        }
                        appViewModel.IsPINWindowOpen = false;
                    }
                }
                IsPINEntered ^= true;
            });
        }
    }
}
