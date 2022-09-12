using Core;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EFRepository<T> : IRepository<T>
         where T : BaseEntity
    {
        readonly EFDbContext _dataContext;
        public EFRepository(EFDbContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task AddAsync(T item)
        {
            await _dataContext.Set<T>().AddAsync(item);
            await _dataContext.SaveChangesAsync();
        }
        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            await _dataContext.Set<T>().AddRangeAsync(items);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dataContext.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await _dataContext.Set<T>().Where(predicate).ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task RemoveAsync(T item)
        {
            _dataContext.Set<T>().Remove(item);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T item)
        {
            await _dataContext.SaveChangesAsync();
        }
    }

}
