namespace sarisari
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : WpfCore.BasePage<MainPageViewModel>
    {
        public MainPage(MainPageViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            SlideSeconds = 1f;
            PageLoadAnimation = WpfCore.PageAnimation.SlideAndFadeInFromRight;
        }
    }
}
