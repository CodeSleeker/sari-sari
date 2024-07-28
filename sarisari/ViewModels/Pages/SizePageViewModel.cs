using System;
using System.Threading.Tasks;
using System.Windows.Input;
using static sarisari.AppExtension;
namespace sarisari
{
    public class SizePageViewModel : WpfCore.BaseViewModel
    {
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand AddSize { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand CommandDialog { get; set; }
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set { _ButtonText = value; OnPropertyChanged(); }
        }
        private string _DialogTitle;

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value; OnPropertyChanged(); }
        }
        private bool _IsAddSizeDialogOpen;

        public bool IsAddSizeDialogOpen
        {
            get { return _IsAddSizeDialogOpen; }
            set { _IsAddSizeDialogOpen = value; OnPropertyChanged(); }
        }
        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Description;
        public string Description { get { return _Description; } set { _Description = value; OnPropertyChanged(); } }
        private bool _CheckInput;
        public bool CheckInput { get { return _CheckInput; } set { _CheckInput = value; OnPropertyChanged(); } }
        private bool _ClearInput;
        public bool ClearInput { get { return _ClearInput; } set { _ClearInput = value; OnPropertyChanged(); } }
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value; OnPropertyChanged(); } }
        Size size;
        public SizePageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Sizes.Count > 0 ? true : false;
            appViewModel.EditButtonVisibility = true;
            //foreach (var item in appViewModel.Sizes)
            //{
            //    item.IsUpdating = false;
            //}
            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await SizeStore.UpdateSizeAsync(param as Size);
            //    foreach (var item in appViewModel.Sizes)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var sizes = appViewModel.Sizes;
            //    appViewModel.Sizes = new System.Collections.ObjectModel.ObservableCollection<Size>(sizes);
            //    appViewModel.EditButtonVisibility = true;
            //});
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                size = param as Size;
                foreach (var item in appViewModel.Products)
                {
                    if (item.Size == size)
                    {
                        appViewModel.DialogText = "This size is in used!";
                        appViewModel.IsDialogOpen = true;
                        return;
                    }
                }
                foreach (var item in appViewModel.Sizes)
                {
                    if (item == size)
                    {
                        await ServiceExtension<Size, DataDbContext>.Store.RemoveData(item);
                        appViewModel.Sizes.Remove(item);
                        ListViewVisibility = appViewModel.Sizes.Count > 0 ? true : false;
                        return;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                size = param as Size;
                Name = size.Name;
                Description = size.Description;
                ButtonText = "UPDATE";
                DialogTitle = "UPDATE SIZE";
                IsAddSizeDialogOpen = true;
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
                        size = new Size
                        {
                            Name = Name,
                            Description = Description,
                            IsUpdating = false,
                            DateAdded = DateTime.Now,
                            DateModified = DateTime.Now
                        };
                        await ServiceExtension<Size, DataDbContext>.Store.AddData(size);
                        appViewModel.Sizes.Add(size);
                    }
                    else
                    {
                        size.Name = Name;
                        size.Description = Description;
                        size.DateModified = DateTime.Now;
                        await ServiceExtension<Size, DataDbContext>.Store.UpdateData(size);
                    }
                    ListViewVisibility = true;
                    IsAddSizeDialogOpen = false;
                    IsPageEnabled = true;
                }
            });
            AddSize = new WpfCore.RelayCommand(() =>
            {
                ClearInput = true;
                ButtonText = "ADD";
                DialogTitle = "ADD SIZE";
                IsAddSizeDialogOpen = true;
            });
            Cancel = new WpfCore.RelayCommand(() =>
            {
                IsAddSizeDialogOpen = false;
                IsPageEnabled = true;
            });
            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddSizeDialogOpen;
            });
        }
    }
}
