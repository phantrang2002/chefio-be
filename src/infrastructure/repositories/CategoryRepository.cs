using Chefio.Domain.Entities;
using Chefio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 
using Chefio.Application.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Chefio.Application.Dtos;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;


namespace Chefio.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ChefioDbContext _context;

    public CategoryRepository(ChefioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Categories
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    } 

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    } 
    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Category category)
    {
        _context.Categories.Remove(category);
        await Task.CompletedTask;
    }

}
