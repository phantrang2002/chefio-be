using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Repositories
{
    public interface ITableRepository
    {
        Task AddAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(Table table);
        Task<Table> GetByIdAsync(int id);
        Task<IEnumerable<Table>> GetAllAsync(int page, int pageSize);
        Task<int> GetNextTableNumberAsync();
        Task SaveChangesAsync();
    }
}