using Microsoft.Extensions.DependencyInjection;

namespace sarisari
{
    public static class AppExtension
    {
        public static ServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<ApplicationViewModel>()
            .BuildServiceProvider();
        public static ApplicationViewModel appViewModel = serviceProvider.GetService<ApplicationViewModel>();
    }
}
