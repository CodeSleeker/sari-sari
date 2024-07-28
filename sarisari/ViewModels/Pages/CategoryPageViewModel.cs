using System;
using System.Threading.Tasks;
using System.Windows.Input;
using static sarisari.AppExtension;
namespace sarisari
{
    public class CategoryPageViewModel : WpfCore.BaseViewModel
    {
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }
        public ICommand SaveUpdate { get; set; }
        public ICommand AddCategory { get; set; }
        public ICommand CommandDialog { get; set; }
        public ICommand Cancel { get; set; }
        private bool _IsAddCategoryDialogOpen;
        private bool _IsPageEnabled = true;
        public bool IsPageEnabled { get { return _IsPageEnabled; } set { _IsPageEnabled = value; OnPropertyChanged(); } }

        public bool IsAddCategoryDialogOpen
        {
            get { return _IsAddCategoryDialogOpen; }
            set { _IsAddCategoryDialogOpen = value; OnPropertyChanged(); }
        }
        private string _DialogTitle = "ADD CATEGORY";

        public string DialogTitle
        {
            get { return _DialogTitle; }
            set { _DialogTitle = value; OnPropertyChanged(); }
        }
        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set { _ButtonText = value; OnPropertyChanged(); }
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
        Category category;
        public CategoryPageViewModel()
        {
            appViewModel.Processing = true;
            ListViewVisibility = appViewModel.Categories.Count > 0 ? true : false;
            //appViewModel.EditButtonVisibility = true;
            //foreach (var item in appViewModel.Categories)
            //{
            //    item.IsUpdating = false;
            //}
            //SaveUpdate = new WpfCore.RelayParameterCommand(async (param) =>
            //{
            //    await CategoryStore.UpdateCategoryAsync(param as Category);
            //    foreach (var item in appViewModel.Categories)
            //    {
            //        item.IsUpdating = false;
            //    }
            //    var categories = appViewModel.Categories;
            //    appViewModel.Categories = new System.Collections.ObjectModel.ObservableCollection<Category>(categories);
            //    appViewModel.EditButtonVisibility = true;
            //});
            Delete = new WpfCore.RelayParameterCommand(async (param) =>
            {
                category = param as Category;
                foreach (var item in appViewModel.Products)
                {
                    if (item.Category == category)
                    {
                        appViewModel.DialogText = "This category is in used!";
                        appViewModel.IsDialogOpen = true;
                        return;
                    }
                }
                foreach (var item in appViewModel.Categories)
                {
                    if (item == category)
                    {
                        await ServiceExtension<Category, DataDbContext>.Store.RemoveData(item);
                        appViewModel.Categories.Remove(item);
                        ListViewVisibility = appViewModel.Categories.Count > 0 ? true : false;
                        return;
                    }
                }
            });
            Edit = new WpfCore.RelayParameterCommand((param) =>
            {
                category = param as Category;
                Name = category.Name;
                Description = category.Description;
                ButtonText = "UPDATE";
                DialogTitle = "UPDATE CATEGORY";
                IsAddCategoryDialogOpen = true;
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
                        category = new Category
                        {
                            Name = Name,
                            Description = Description,
                            IsUpdating = false,
                            DateAdded = DateTime.Now,
                            DateModified = DateTime.Now
                        };
                        await ServiceExtension<Category, DataDbContext>.Store.AddData(category);
                        appViewModel.Categories.Add(category);
                    }
                    else
                    {
                        category.Name = Name;
                        category.Description = Description;
                        category.DateModified = DateTime.Now;
                        await ServiceExtension<Category, DataDbContext>.Store.UpdateData(category);
                    }
                    ListViewVisibility = true;
                    IsAddCategoryDialogOpen = false;
                    IsPageEnabled = true;
                }
            });
            AddCategory = new WpfCore.RelayCommand(() =>
            {
                ButtonText = "ADD";
                DialogTitle = "ADD CATEGORY";
                ClearInput = true;
                IsAddCategoryDialogOpen = true;
                IsPageEnabled = false;
            });
            Cancel = new WpfCore.RelayCommand(() =>
            {
                IsAddCategoryDialogOpen = false;
                IsPageEnabled = true;
            });

            CommandDialog = new WpfCore.RelayCommand(() =>
            {
                appViewModel.IsDialogOpen = false;
                IsPageEnabled = !IsAddCategoryDialogOpen;
            });
        }
    }
}
