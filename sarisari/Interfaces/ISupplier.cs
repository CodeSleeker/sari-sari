using System.Collections.Generic;
using System.Threading.Tasks;

namespace sarisari
{
    public interface ISupplier
    {
        Task<bool> IsRunning();
        Task EnsureSupplierStoreAsync();
        Task<Supplier> GetSupplierAsync();
        Task<List<Supplier>> GetSuppliersAsync();
        Task SaveSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task AddSupplierAsync(Supplier supplier);
        Task RemoveSupplierAsync(Supplier supplier);
    }
}
