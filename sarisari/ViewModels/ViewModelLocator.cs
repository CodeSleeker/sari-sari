using static sarisari.AppExtension;
namespace sarisari
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();
        public static ApplicationViewModel ApplicationViewModel => appViewModel;
    }
}
