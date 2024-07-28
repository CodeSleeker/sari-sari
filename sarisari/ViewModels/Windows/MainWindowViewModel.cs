using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static sarisari.AppExtension;

namespace sarisari
{
    public class MainWindowViewModel : WpfCore.BaseViewModel, INotifyPropertyChanged
    {
        private Window window;
        public ICommand SidePanelCommand { get; set; }
        public ICommand SubPanelCommand { get; set; }
        //private bool _IsMaximize = true;
        //public bool IsMaximize { get { return _IsMaximize; } set { _IsMaximize = value; OnPropertyChanged(); } }
        public ICommand Exit { get; set; }
        public ICommand Maximize { get; set; }
        public ICommand Minimize { get; set; }
        public ICommand CommandDialog { get; set; }
        public ICommand SwitchButton { get; set; }
        private bool _IsMaximize = true;
        public bool IsMaximize { get { return window.WindowState == WindowState.Maximized ? true : false; } set { _IsMaximize = value; } }
        private int _SelectedPanel = 1;

        public int SelectedPanel
        {
            get { return _SelectedPanel; }
            set { _SelectedPanel = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
            window.StateChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(IsMaximize));
            };

            window.Closing += (s, e) =>
            {
                if (appViewModel.IsPINWindowOpen) e.Cancel = true;
            };

            appViewModel.Window = window;

            window.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            window.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            Exit = new WpfCore.RelayCommand(() => 
            { 
                if (!appViewModel.IsWindowOpen<Window>("login")) window.Close(); 
            });
            Maximize = new WpfCore.RelayCommand(() =>
            {
                window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                OnPropertyChanged(nameof(IsMaximize));
            });
            Minimize = new WpfCore.RelayCommand(() => window.WindowState = WindowState.Minimized);
            CommandDialog = new WpfCore.RelayCommand(() => appViewModel.IsDialogOpen = false);
            SubPanelCommand = new WpfCore.RelayParameterCommand((value) =>
            {
                switch (value.ToString())
                {
                    case "Product":
                        appViewModel.GotoPage(ApplicationPage.ProductPage);
                        break;
                    case "Category":
                        appViewModel.GotoPage(ApplicationPage.CategoryPage);
                        break;
                    case "Size":
                        appViewModel.GotoPage(ApplicationPage.SizePage);
                        break;
                    case "Supplier":
                        appViewModel.GotoPage(ApplicationPage.SupplierPage);
                        break;
                    case "Stock":
                        appViewModel.GotoPage(ApplicationPage.StockPage);
                        break;
                    case "User":
                        appViewModel.GotoPage(ApplicationPage.UserPage);
                        break;
                    case "User Role":
                        appViewModel.GotoPage(ApplicationPage.RolePage);
                        break;
                    default:
                        break;
                }
            });
            SidePanelCommand = new WpfCore.RelayParameterCommand((value) =>
            {
                var panel = value as SidePanel;
                foreach (var item in appViewModel.SidePanels)
                {
                    if (item.Name != panel.Name)
                    {
                        item.RightIcon = "MenuRight";
                        item.ListViewVisibility = false;
                    }
                    else
                    {
                        if (panel.RightIcon == "MenuRight")
                        {
                            panel.RightIcon = "MenuDown";
                            panel.ListViewVisibility = true;
                        }
                        else
                        {
                            panel.RightIcon = "MenuRight";
                            panel.ListViewVisibility = false;
                        }
                    }
                }
                switch (panel.Name)
                {
                    case "Dashboard":
                        appViewModel.GotoPage(ApplicationPage.MainPage);
                        break;


                    case "Cashier Out":

                        break;
                    default:
                        break;
                }
            });
            SwitchButton = new WpfCore.RelayCommand(() =>
            {
                if (!appViewModel.IsWindowOpen<Window>("login"))
                {
                    var login = new Login();
                    login.DataContext = new LoginViewModel(login, true);
                    login.Show();
                }
            });
        }
    }
}
