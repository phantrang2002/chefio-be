using Chefio.Application.Dtos.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync(int page, int pageSize);

        Task<OrderDto> CreateAsync(OrderCreateRequest request);

        Task<OrderDto> UpdateAsync(int id, OrderUpdateRequest request);

        Task<OrderDto> GetByIdAsync(int id);

        Task<bool> DeleteAsync(int id);

    }
}
