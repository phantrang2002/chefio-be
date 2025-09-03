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

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ChefioDbContext _context;

    public EmployeeRepository(ChefioDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Employees
            .Include(e => e.Account)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Employee> GetByIdAsync(int id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<Employee> GetByAccountIdAsync(int accountId)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.AccountId == accountId);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AccountExistsAsync(int accountId)
    {
        return await _context.Accounts.AnyAsync(a => a.Id == accountId);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await Task.CompletedTask;
    }

}
