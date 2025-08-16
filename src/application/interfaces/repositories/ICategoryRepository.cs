using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks; 


namespace Chefio.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync(int page, int pageSize);

        Task<Category> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task SaveChangesAsync();

        Task DeleteAsync(Category category);
    }
}
