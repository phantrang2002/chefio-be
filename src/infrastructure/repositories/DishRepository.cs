using Chefio.Domain.Entities;
using Chefio.Infrastructure.Data;
using Chefio.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Chefio.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly ChefioDbContext _context;

        public DishRepository(ChefioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Dishes
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            return await _context.Dishes.FindAsync(id);
        }

        public async Task AddAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
        }

        public async Task UpdateAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Dish dish)
        {
            _context.Dishes.Remove(dish);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}