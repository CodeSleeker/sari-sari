using System.Collections.Generic;
using System.Threading.Tasks;

namespace sarisari
{
    public interface IRepository<T>
    {
        Task<T> GetData(int index);
        Task<List<T>> GetAllData();
        Task UpdateData(T item);
        Task AddData(T item);
        Task RemoveData(T item);
        Task RemoveAllData();
    }
}
