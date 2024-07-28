using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace sarisari
{
    public static class TestExtension<T> 
        where T:class
    {
        public static ServiceProvider service = new ServiceCollection()
            .AddDbContext<DataDbContext>(options =>
            {
                options.UseSqlite($@"Data Source = {App.path}\SariSari.db");
            })
            .AddScoped<IRepository<T>>(provider => new Repository<T>(provider.GetService<DataDbContext>()))
            .BuildServiceProvider();
        public static IRepository<T> ModelStore = service.GetService<IRepository<T>>();

    }
}
