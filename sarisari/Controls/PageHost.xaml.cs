using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace sarisari
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost: UserControl
    {
        public PageHost()
        {
            InitializeComponent();
        }
        #region Dependency Property
        public ApplicationPage CurrentPage
        {
            get { return (ApplicationPage)GetValue(CurrentPageProperty); }
            set { SetValue(CurrentPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage),
                typeof(ApplicationPage), typeof(PageHost),
                new UIPropertyMetadata(default(ApplicationPage), null, CurrentPagePropertyChanged));

        public WpfCore.BaseViewModel CurrentPageViewModel
        {
            get { return (WpfCore.BaseViewModel)GetValue(CurrentPageViewModelProperty); }
            set { SetValue(CurrentPageViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageViewModelProperty =
            DependencyProperty.Register(nameof(CurrentPageViewModel),
                typeof(WpfCore.BaseViewModel), typeof(PageHost),
                new UIPropertyMetadata());
        #endregion

        #region Dependency Property Changed
        private static object CurrentPagePropertyChanged(DependencyObject d, object value)
        {
            var currentPage = (ApplicationPage)value;
            var currentPageViewModel = d.GetValue(CurrentPageViewModelProperty);

            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            var oldPageContent = newPageFrame.Content;
            newPageFrame.Content = null;
            oldPageFrame.Content = oldPageContent;

            if (oldPageContent is WpfCore.BasePage oldPage)
            {
                oldPage.ShouldAnimateOut = true;
                oldPage.PageUnloadAnimation = WpfCore.PageAnimation.SlideAndFadeOutToLeft;
                oldPage.SlideSeconds = 1f;
                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((t) =>
                {
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }
            newPageFrame.Content = currentPage.ToBasePage(currentPageViewModel);
            return value;
        }
        #endregion
    }
}
