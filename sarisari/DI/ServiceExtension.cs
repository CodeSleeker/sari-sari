using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static sarisari.AppExtension;
using System;

namespace sarisari
{
    public static class ServiceExtension<T,M> where T : class where M:DbContext
    { 
        public static ServiceProvider service = new ServiceCollection()
            .AddDbContext<M>(options =>
            {
                options.UseSqlite($@"Data Source = {Constants.dbLocation}");
            })
            .AddScoped<IRepository<T>>(provider => new Repository<T,M>(provider.GetService<M>()))
            .BuildServiceProvider();
        public static IRepository<T> Store = service.GetService<IRepository<T>>();
    }
}
