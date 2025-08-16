using Chefio.Application.Dtos.Employee;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chefio.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(int page, int pageSize)
        {
            var employees = await _repository.GetAllAsync(page, pageSize);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = e.FullName,
                AccountId = e.AccountId,
                Address = e.Address,
                Note = e.Note,
            });
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Address = employee.Address,
                Note = employee.Note,
                AccountId = employee.AccountId
            };
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeCreateRequest request)
        {
            var accountExists = await _repository.AccountExistsAsync(request.AccountId);

            if (!accountExists)
                throw new ArgumentException("Account Id does not exist.");

            var existingEmployee = await _repository.GetByAccountIdAsync(request.AccountId);
            if (existingEmployee != null)
                throw new ArgumentException($"Account Id {request.AccountId} is linked to employee: {existingEmployee.FullName} (Id: {existingEmployee.Id})");


            var employee = new Employee
            {
                FullName = request.FullName,
                Address = request.Address,
                Note = request.Note,
                AccountId = request.AccountId
            };

            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Address = employee.Address,
                Note = employee.Note,
                AccountId = employee.AccountId
            };
        }

        public async Task<EmployeeDto> UpdateAsync(int id, EmployeeUpdateRequest request)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return null;

            employee.FullName = request.FullName;
            employee.Address = request.Address;
            employee.Note = request.Note;
            employee.UpdatedAt = DateTime.UtcNow;

            if (request.AccountId.HasValue)
            {
                if (!await _repository.AccountExistsAsync(request.AccountId.Value))
                    throw new ArgumentException("Account Id does not exist.");

                var existingEmployee = await _repository.GetByAccountIdAsync(request.AccountId.Value);
                if (existingEmployee != null && existingEmployee.Id != id)
                    throw new ArgumentException($"Account Id {request.AccountId.Value} is linked to employee: {existingEmployee.FullName} (Id: {existingEmployee.Id})");

                employee.AccountId = request.AccountId.Value;
            }

            await _repository.UpdateAsync(employee);
            await _repository.SaveChangesAsync();

            return new EmployeeDto
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Address = employee.Address,
                Note = employee.Note,
                AccountId = employee.AccountId
            };
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
                return false;

            await _repository.DeleteAsync(employee);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
