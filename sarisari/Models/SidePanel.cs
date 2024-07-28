using System.Collections.Generic;
using WpfCore;

namespace sarisari
{
    public class SidePanel: BaseViewModel
    {
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Icon;
        public string Icon { get { return _Icon; } set { _Icon = value; OnPropertyChanged(); } }
        private string _RightIcon = "MenuRight";
        public string RightIcon { get { return _RightIcon; } set { _RightIcon = value; OnPropertyChanged(); } }
        private bool _RightIconVisibility;
        public bool RightIconVisibility { get { return _RightIconVisibility; } set { _RightIconVisibility = value; OnPropertyChanged(); } }
        private bool _ListViewVisibility;
        public bool ListViewVisibility { get { return _ListViewVisibility; } set { _ListViewVisibility = value; OnPropertyChanged(); } }
        private bool _IsUser;
        public bool IsUser { get { return _IsUser; } set { _IsUser = value; OnPropertyChanged(); } }
        private bool _IsAdmin;
        public bool IsAdmin { get { return _IsAdmin; } set { _IsAdmin = value; OnPropertyChanged(); } }
        private List<string> _SubPanel;
        public List<string> SubPanel { get { return _SubPanel; } set { _SubPanel = value; OnPropertyChanged(); } }
    }
}
