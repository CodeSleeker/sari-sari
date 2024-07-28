using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static sarisari.AppExtension;

namespace sarisari
{
    public class RolePageViewModel : WpfCore.BaseViewModel
    {
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand AddRole { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set { _ButtonText = value;OnPropertyChanged(); }
        }

        private string _DialogTitle;

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value;OnPropertyChanged(); }
        }


        private bool _IsAddRoleDialogOpen;
        public bool IsAddRoleDialogOpen { get { return _IsAddRoleDialogOpen; } set { _IsAddRoleDialogOpen = value;OnPropertyChanged(); } }
        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Description;
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged(); } }
        Role role;
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value; OnPropertyChanged(); } }
        public RolePageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Roles.Count > 0 ? true : false;

            //foreach (var item in appViewModel.Roles)
            //{
            //    item.IsUpdating = false;
            //}
            //appViewModel.EditButtonVisibility = true;

            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await ServiceExtension<Role>.Store.UpdateData(param as Role);
            //    foreach (var item in appViewModel.Roles)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var roles = appViewModel.Roles;
            //    appViewModel.Roles = new List<Role>(roles);
            //    appViewModel.EditButtonVisibility = true;
            //});
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                role = param as Role;
                foreach (var item in appViewModel.Users)
                {
                    if (item.Role == role)
                    {
                        appViewModel.DialogText = "This role is in used!";
                        appViewModel.IsDialogOpen = true;
                        IsPageEnabled = false;
                        return;
                    }
                }
                foreach (var item in appViewModel.Roles)
                {
                    if (item == role)
                    {
                        await ServiceExtension<Role, DataDbContext>.Store.RemoveData(item);
                        appViewModel.Roles.Remove(item);
                        ListViewVisibility = appViewModel.Roles.Count > 0 ? true : false;
                        return;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
              {
                  role = param as Role;
                  Name = role.Name;
                  Description = role.Description;
                  ButtonText = "UPDATE";
                  DialogTitle = "UPDATE ROLE";
                  IsAddRoleDialogOpen = true;
                  IsPageEnabled = false;
              });
            Add = new WpfCore.RelayCommand(async () =>
             {
                 CheckInput = true;
                 await Task.Run(async () =>
                 {
                     while (appViewModel.Processing) { await Task.Delay(1000); }
                 });
                 if (!appViewModel.IsDialogOpen)
                 {
                     if (ButtonText == "ADD")
                     {
                         role = new Role { 
                             Name = Name, 
                             Description = Description, 
                             DateAdded = DateTime.Now,
                             DateModified = DateTime.Now
                         };
                         await ServiceExtension<Role, DataDbContext>.Store.AddData(role);
                         appViewModel.Roles.Add(role);
                     }
                     else
                     {
                         role.Name = Name;
                         role.Description = Description;
                         role.DateModified = DateTime.Now;
                         await ServiceExtension<Role, DataDbContext>.Store.UpdateData(role);
                     }
                     ListViewVisibility = true;
                     IsAddRoleDialogOpen = false;
                 }
             });
            AddRole = new WpfCore.RelayCommand(() =>
            {
                ClearInput = true;
                ButtonText = "ADD";
                DialogTitle = "ADD ROLE";
                IsAddRoleDialogOpen = true;
                IsPageEnabled = false;
            });
            Cancel = new WpfCore.RelayCommand(() =>
            {
                IsAddRoleDialogOpen = false;
                IsPageEnabled = true;
            });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddRoleDialogOpen;
            });
        }
    }
}
