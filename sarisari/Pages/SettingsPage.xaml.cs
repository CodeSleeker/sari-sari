namespace sarisari
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : WpfCore.BasePage<SettingsPageViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
        public SettingsPage(SettingsPageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
