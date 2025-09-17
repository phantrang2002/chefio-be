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

public class OrderRepository : IOrderRepository
{
    private readonly ChefioDbContext _context;

    public OrderRepository(ChefioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Orders
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    } 

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    } 
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await Task.CompletedTask;
    }

}
