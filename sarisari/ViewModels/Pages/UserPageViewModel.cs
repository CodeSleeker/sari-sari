using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static sarisari.AppExtension;

namespace sarisari
{
    public class UserPageViewModel : WpfCore.BaseViewModel
    {
        private string _Username;
        public string Username { get { return _Username; } set { _Username = value; OnPropertyChanged(); } }
        private string _Fname;
        public string FName { get { return _Fname; } set { _Fname = value;OnPropertyChanged(); } }
        private string _LName;
        public string LName
        {
            get { return _LName; }
            set { _LName = value;OnPropertyChanged(); }
        }

        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private Role _SelectedRole;
        public Role SelectedRole { get { return _SelectedRole; } set { _SelectedRole = value; OnPropertyChanged(); } }
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand AddUser { get; set; }
        public ICommand CommandDialog { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Close { get; set; }
        private string _DialogTitle = "ADD USER";
        public string DialogTitle { get { return _DialogTitle; } set { _DialogTitle = value; OnPropertyChanged(); } }
        private string _ButtonText = "ADD";
        public string ButtonText { get { return _ButtonText; } set { _ButtonText = value; OnPropertyChanged(); } }
        private bool _IsUpdating = false;
        public bool IsUpdating { get { return _IsUpdating; } set { _IsUpdating = value; OnPropertyChanged(); } }
        private bool _IsAddUserDialogOpen;
        public bool IsAddUserDialogOpen { get { return _IsAddUserDialogOpen; } set { _IsAddUserDialogOpen = value;OnPropertyChanged(); } }
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _CheckComboBox;
        public bool CheckComboBox { get { return _CheckComboBox; } set { _CheckComboBox = value; OnPropertyChanged(); } }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value;OnPropertyChanged(); } }
        User user;
        public UserPageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Users.Count > 0;
            //foreach (var item in appViewModel.Users)
            //{
            //    item.IsUpdating = false;
            //}
            //appViewModel.EditButtonVisibility = true;
            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await UserStore.UpdateUserAsync(param as User);
            //    foreach (var item in appViewModel.Users)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var users = appViewModel.Users;
            //    appViewModel.Users = new ObservableCollection<User>(users);
            //    appViewModel.EditButtonVisibility = true;
            //});
            //Close = new WpfCore.RelayCommand(() => {
            //    foreach (var item in appViewModel.Users)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var users = appViewModel.Users;
            //    appViewModel.Users = new ObservableCollection<User>(users);
            //    appViewModel.EditButtonVisibility = true; 
            //});
            Delete = new WpfCore.RelayParameterCommand(async(param) =>
            {
                user = param as User;
                if(appViewModel.Drawers.Where(x=>x.CashierId == user.Id).Any())
                {
                    appViewModel.DialogText = "Cannot delete cashier";
                    appViewModel.IsDialogOpen = true;
                    IsPageEnabled = false;
                    return;
                }
                if(appViewModel.User == user)
                {
                    appViewModel.DialogText = "Cannot delete current user!";
                    appViewModel.IsDialogOpen = true;
                    IsPageEnabled = false;
                    return;
                }
                foreach (var item in appViewModel.Users)
                {
                    if(item == user)
                    {
                        appViewModel.Users.Remove(item);
                        await ServiceExtension<User, DataDbContext>.Store.RemoveData(item);
                        ListViewVisibility = appViewModel.Users.Count > 0;
                        return;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                IsUpdating = true;
                user = param as User;
                FName = user.FirstName;
                LName = user.LastName;
                SelectedRole = user.Role;
                ButtonText = "UPDATE";
                DialogTitle = "UPDATE USER";
                IsAddUserDialogOpen = true;
                IsPageEnabled = false;
            });
            Add = new WpfCore.RelayParameterCommand(async (param) =>
              {
                  appViewModel.Processing = true;
                  CheckInput = true;
                  await Task.Run(async () =>
                  {
                      while (appViewModel.Processing) { await Task.Delay(1000); }
                  });
                  IsPageEnabled = !appViewModel.IsDialogOpen;
                  if (!appViewModel.IsDialogOpen)
                  {
                      appViewModel.Processing = true;
                      CheckComboBox = true;
                      await Task.Run(async () =>
                      {
                          while (appViewModel.Processing) { await Task.Delay(1000); }
                      });
                      IsPageEnabled = !appViewModel.IsDialogOpen;
                      if (!appViewModel.IsDialogOpen)
                      {
                          if (ButtonText == "ADD")
                          {

                              if ((param as ICPassword).SecurePassword.Unsecure() == (param as ICPassword).SecureCPassword.Unsecure())
                              {
                                  var hashPassword = PasswordHelper.HashPassword((param as ICPassword).SecurePassword.Unsecure());
                                  var user = new User
                                  {
                                      FirstName = FName,
                                      LastName = LName,
                                      PIN = hashPassword,
                                      Role = SelectedRole,
                                      DateAdded = DateTime.Now,
                                      DateModified = DateTime.Now
                                  };
                                  await ServiceExtension<User, DataDbContext>.Store.AddData(user);
                                  appViewModel.Users.Add(user);
                                  
                              }
                              else
                              {
                                  appViewModel.DialogText = "Password Mismatch!";
                                  appViewModel.IsDialogOpen = true;
                                  return;
                              }
                          }
                          else
                          {
                              user.FirstName = FName;
                              user.LastName = LName;
                              user.Role = SelectedRole;
                              user.DateModified = DateTime.Now;
                              await ServiceExtension<User, DataDbContext>.Store.UpdateData(user);
                          }
                          ListViewVisibility = true;
                          IsAddUserDialogOpen = false;
                          IsUpdating = false;
                          IsPageEnabled = true;
                      }
                  }
              });
            AddUser = new WpfCore.RelayCommand(()=>
            {
                ClearInput = true;
                IsUpdating = false;
                ButtonText = "ADD";
                DialogTitle = "ADD USER";
                if (appViewModel.Roles.Count == 0)
                {
                    appViewModel.DialogText = "Please add a role first before creating a new user.";
                    appViewModel.IsDialogOpen = true;
                    IsPageEnabled = false;
                }
                else
                {
                    IsAddUserDialogOpen = true;
                    IsPageEnabled = false;
                }
                //string touchKeyboardPath = @"C:\Program Files\Common Files\Microsoft Shared\Ink\TabTip.exe";
                //var processes = Process.GetProcessesByName("TabTip");
                //if (processes != null) processes.ToList().ForEach(x => x.Kill());
                //Process.Start(touchKeyboardPath);
                
            });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                if(!IsAddUserDialogOpen) IsPageEnabled = true;
            });
            Cancel = new WpfCore.RelayCommand(() => {
                IsAddUserDialogOpen = false;
                IsPageEnabled = true;
            });
        }
    }
}
