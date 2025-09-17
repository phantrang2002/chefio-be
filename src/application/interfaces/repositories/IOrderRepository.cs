using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks; 


namespace Chefio.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(int page, int pageSize);

        Task<Order> GetByIdAsync(int id);

        Task AddAsync(Order category);

        Task UpdateAsync(Order category);

        Task SaveChangesAsync();

        Task DeleteAsync(Order category);
    }
}
