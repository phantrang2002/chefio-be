using Chefio.Application.Dtos.Dish;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace Chefio.Application.Interfaces.Services
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto>> GetAllAsync(int page, int pageSize);
        Task<DishDto> GetByIdAsync(int id);
        Task<DishDto> CreateAsync(DishCreateFormRequest request);
        Task<DishDto> UpdateAsync(int id, DishUpdateFormRequest request);
        Task<bool> DeleteAsync(int id);
    }
}