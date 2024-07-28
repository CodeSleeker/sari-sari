using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace sarisari
{
    public class Repository<T,M> : IRepository<T> where T : class where M: DbContext
    {
        protected M dbContext;
        public Repository(M dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddData(T item)
        {
            await dbContext.Database.EnsureCreatedAsync();
            //dbContext.Set<T>().Add(item);
            dbContext.ChangeTracker.TrackGraph(item, node => node.Entry.State = !node.Entry.IsKeySet ? Microsoft.EntityFrameworkCore.EntityState.Added : Microsoft.EntityFrameworkCore.EntityState.Unchanged);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllData()
        {
            await dbContext.Database.EnsureCreatedAsync();
            return await Task.FromResult(dbContext.Set<T>().ToList());
        }

        public Task<T> GetData(int index)
        {
            return Task.FromResult(dbContext.Set<T>().Find(index));
        }

        public async Task RemoveAllData()
        {
            dbContext.Set<T>().RemoveRange(dbContext.Set<T>());
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveData(T item)
        {
            dbContext.Set<T>().Remove(item);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateData(T item)
        {
            dbContext.Set<T>().Update(item);
            await dbContext.SaveChangesAsync();
        }
    }
}
