using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks; 


namespace Chefio.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int page, int pageSize);

        Task<Employee> GetByIdAsync(int id);

        Task<Employee> GetByAccountIdAsync(int accountId);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task SaveChangesAsync();

        Task<bool> AccountExistsAsync(int accountId);

        Task DeleteAsync(Employee employee);
    }
}
