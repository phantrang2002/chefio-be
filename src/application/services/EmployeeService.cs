using Chefio.Application.Dtos.Employee;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chefio.Application.Constants;

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
                Address = e.Address,
                Note = e.Note,
                AccountId = e.AccountId,
                Account = new AccountDto
                {
                    Id = e.Account.Id,
                    Role = e.Account.Role.ToString()
                }
            }).ToList();
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
                throw new ArgumentException(ApiMessages.ACCOUNT.NOT_FOUND.Message);

            var existingEmployee = await _repository.GetByAccountIdAsync(request.AccountId);
            if (existingEmployee != null)
                throw new ArgumentException($"ID tài khoản {request.AccountId} đã được liên kết với nhân viên: {existingEmployee.FullName} (ID: {existingEmployee.Id})");


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
                    throw new ArgumentException(ApiMessages.ACCOUNT.NOT_FOUND.Message);

                var existingEmployee = await _repository.GetByAccountIdAsync(request.AccountId.Value);
                if (existingEmployee != null && existingEmployee.Id != id)
                    throw new ArgumentException($"ID tài khoản {request.AccountId.Value} đã được liên kết với nhân viên: {existingEmployee.FullName} (ID: {existingEmployee.Id})");

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
