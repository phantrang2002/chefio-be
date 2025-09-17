using Microsoft.AspNetCore.Http;
using Chefio.Application.Dtos.Dish;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chefio.Application.Constants;


namespace Chefio.Application.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public DishService(IDishRepository repository, ICategoryRepository categoryRepository, IFirebaseStorageService firebaseStorageService)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _firebaseStorageService = firebaseStorageService;
        }

        public async Task<IEnumerable<DishDto>> GetAllAsync(int page, int pageSize)
        {
            var dishes = await _repository.GetAllAsync(page, pageSize);
            return dishes.Select(d => new DishDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.description,
                Photo = d.photo,
                Price = d.Price,
                CategoryId = d.CategoryId,
                IsAvailable = d.isAvailable
            });
        }

        public async Task<DishDto> GetByIdAsync(int id)
        {
            var dish = await _repository.GetByIdAsync(id);
            if (dish == null) return null;
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.description,
                Photo = dish.photo,
                Price = dish.Price,
                CategoryId = dish.CategoryId,
                IsAvailable = dish.isAvailable
            };
        }

        public async Task<DishDto> CreateAsync(DishCreateFormRequest request)
        {
            // Kiểm tra CategoryId
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new ArgumentException(ApiMessages.CATEGORY.NOT_FOUND.Message);

            string photoUrl = null;
            if (request.Photo != null && request.Photo.Length > 0)
            {
                photoUrl = await _firebaseStorageService.UploadFileAsync(request.Photo, "dishes");
            }

            var dish = new Dish
            {
                Name = request.Name,
                description = request.Description,
                photo = photoUrl,
                Price = request.Price,
                CategoryId = request.CategoryId,
                isAvailable = true
            };
            await _repository.AddAsync(dish);
            await _repository.SaveChangesAsync();
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.description,
                Photo = dish.photo,
                Price = dish.Price,
                CategoryId = dish.CategoryId,
                IsAvailable = dish.isAvailable
            };
        }

        public async Task<DishDto> UpdateAsync(int id, DishUpdateFormRequest request)
        {
            var dish = await _repository.GetByIdAsync(id);
            if (dish == null) return null;

            // Kiểm tra CategoryId
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new ArgumentException(ApiMessages.CATEGORY.NOT_FOUND.Message);

            string photoUrl = dish.photo;
            if (request.Photo != null && request.Photo.Length > 0)
            {
                photoUrl = await _firebaseStorageService.UploadFileAsync(request.Photo, "dishes");
            }

            dish.Name = request.Name;
            dish.description = request.Description;
            dish.photo = photoUrl;
            dish.Price = request.Price;
            dish.CategoryId = request.CategoryId;
            dish.isAvailable = request.IsAvailable;
            dish.UpdatedAt = DateTime.UtcNow;


            await _repository.UpdateAsync(dish);
            await _repository.SaveChangesAsync();
            return new DishDto
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.description,
                Photo = dish.photo,
                Price = dish.Price,
                CategoryId = dish.CategoryId,
                IsAvailable = dish.isAvailable
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dish = await _repository.GetByIdAsync(id);
            if (dish == null) return false;
            await _repository.DeleteAsync(dish);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}