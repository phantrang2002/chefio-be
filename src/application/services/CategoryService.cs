using Chefio.Application.Dtos.Category;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chefio.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(int page, int pageSize)
        {
            var categories = await _repository.GetAllAsync(page, pageSize);
            return categories.Select(e => new CategoryDto
            {
                Id = e.Id,
                Name = e.Name,
            });
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateRequest request)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return null;

            category.Name = request.Name;
            category.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(category);
            await _repository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _repository.DeleteAsync(category);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
