using Chefio.Domain.Entities;
using Chefio.Application.Interfaces.Repositories;
using Chefio.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chefio.Infrastructure.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ChefioDbContext _context;

        public TableRepository(ChefioDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
        }

        public async Task UpdateAsync(Table table)
        {
            _context.Tables.Update(table);
        }

        public async Task DeleteAsync(Table table)
        {
            _context.Tables.Remove(table);
        }

        public async Task<Table> GetByIdAsync(int id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<IEnumerable<Table>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Tables
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetNextTableNumberAsync()
        {
            var lastTable = await _context.Tables.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            return lastTable == null ? 1 : lastTable.Id + 1;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}