using Chefio.Application.Dtos.Table;
using Chefio.Application.Interfaces.Services;
using Chefio.Domain.Entities;
using Chefio.Application.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chefio.Application.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _repository;

        public TableService(ITableRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TableDto>> CreateTablesAsync(int quantity)
        {
            var tables = new List<Table>();
            var nextNumber = await _repository.GetNextTableNumberAsync();

            for (int i = 0; i < quantity; i++)
            {
                var table = new Table
                {
                    Name = $"Table {nextNumber + i}",
                    isAvailable = true
                };
                await _repository.AddAsync(table);
                tables.Add(table);
            }
            await _repository.SaveChangesAsync();

            // Sau khi lưu, Id đã được sinh ra, trả về đúng thông tin
            return tables.Select(t => new TableDto
            {
                Id = t.Id,
                Name = t.Name,
                IsAvailable = t.isAvailable
            }).ToList();
        }

        public async Task<TableDto> UpdateStatusAsync(int id, bool isAvailable)
        {
            var table = await _repository.GetByIdAsync(id);
            if (table == null) return null;
            
            table.isAvailable = isAvailable;
            table.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(table);
            await _repository.SaveChangesAsync();
            return new TableDto
            {
                Id = table.Id,
                Name = table.Name,
                IsAvailable = table.isAvailable
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var table = await _repository.GetByIdAsync(id);
            if (table == null) return false;
            await _repository.DeleteAsync(table);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TableDto>> GetAllAsync(int page, int pageSize)
        {
            var tables = await _repository.GetAllAsync(page, pageSize);
            var result = new List<TableDto>();
            foreach (var table in tables)
            {
                result.Add(new TableDto
                {
                    Id = table.Id,
                    Name = table.Name,
                    IsAvailable = table.isAvailable
                });
            }
            return result;
        }

        public async Task<TableDto> GetByIdAsync(int id)
        {
            var table = await _repository.GetByIdAsync(id);
            if (table == null) return null;
            return new TableDto
            {
                Id = table.Id,
                Name = table.Name,
                IsAvailable = table.isAvailable
            };
        }
    }
}