using Chefio.Application.Dtos.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Interfaces.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> CreateTablesAsync(int quantity);
        Task<TableDto> UpdateStatusAsync(int id, bool isAvailable);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TableDto>> GetAllAsync(int page, int pageSize);
        Task<TableDto> GetByIdAsync(int id);
    }
}