using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Repositories
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync(int page, int pageSize);
        Task<Dish> GetByIdAsync(int id);
        Task AddAsync(Dish dish);
        Task UpdateAsync(Dish dish);
        Task DeleteAsync(Dish dish);
        Task SaveChangesAsync();
    }
}