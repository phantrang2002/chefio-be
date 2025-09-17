using Chefio.Application.Dtos.Order;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chefio.Domain.Common;
using Chefio.Application.Constants;
using System;

namespace Chefio.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly ITableRepository _tableRepository;

        public OrderService(IOrderRepository repository, IEmployeeRepository employeeRepository, ITableRepository tableRepository)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync(int page, int pageSize)
        {
            var orders = await _repository.GetAllAsync(page, pageSize);
            return orders.Select(e => new OrderDto
            {
                Id = e.Id,
                EmployeeId = e.EmployeeId,
                TableId = e.TableId,
                TimeIn = e.TimeIn,
                TimeOut = e.TimeOut,
                Status = e.Status,
            });
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return null;

            return new OrderDto
            {
                Id = order.Id,
                EmployeeId = order.EmployeeId,
                TableId = order.TableId,
                TimeIn = order.TimeIn,
                TimeOut = order.TimeOut,
                Status = order.Status,
            };
        }

        public async Task<OrderDto> CreateAsync(OrderCreateRequest request)
        {
            // Kiểm tra EmployeeId
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                throw new ArgumentException(ApiMessages.EMPLOYEE.NOT_FOUND.Message);

            // Kiểm tra TableId
            var table = await _tableRepository.GetByIdAsync(request.TableId);
            if (table == null)
                throw new ArgumentException(ApiMessages.TABLE.NOT_FOUND.Message);

            var order = new Order
            {
                EmployeeId = request.EmployeeId,
                TableId = request.TableId,
                TimeIn = DateTime.UtcNow,
                Status = OrderStatus.Pending,
            };

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                EmployeeId = order.EmployeeId,
                TableId = order.TableId,
                TimeIn = order.TimeIn,
                Status = order.Status,
            };
        }

        public async Task<OrderDto> UpdateAsync(int id, OrderUpdateRequest request)
        {
            // Kiểm tra EmployeeId
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
                throw new ArgumentException(ApiMessages.EMPLOYEE.NOT_FOUND.Message);

            // Kiểm tra TableId
            var table = await _tableRepository.GetByIdAsync(request.TableId);
            if (table == null)
                throw new ArgumentException(ApiMessages.TABLE.NOT_FOUND.Message);

            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return null;

            order.EmployeeId = request.EmployeeId;
            order.TableId = request.TableId;
            order.TimeIn = request.TimeIn;
            order.TimeOut = request.TimeOut;
            order.Status = request.Status;
            order.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(order);
            await _repository.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                EmployeeId = order.EmployeeId,
                TableId = order.TableId,
                TimeIn = order.TimeIn,
                TimeOut = order.TimeOut,
                Status = order.Status,
            };
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
                return false;

            await _repository.DeleteAsync(order);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
