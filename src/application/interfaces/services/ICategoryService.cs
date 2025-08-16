using Chefio.Application.Dtos.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync(int page, int pageSize);

        Task<CategoryDto> CreateAsync(CategoryCreateRequest request);

        Task<CategoryDto> UpdateAsync(int id, CategoryUpdateRequest request);

        Task<CategoryDto> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

    }
}
