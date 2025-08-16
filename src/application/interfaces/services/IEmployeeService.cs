using Chefio.Application.Dtos.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync(int page, int pageSize);

        Task<EmployeeDto> CreateAsync(EmployeeCreateRequest request);

        Task<EmployeeDto> UpdateAsync(int id, EmployeeUpdateRequest request);

        Task<EmployeeDto> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

    }
}
