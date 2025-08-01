using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SoftBank.Core.Repositories;

namespaсe SoftBank.Infrastructure.EntityFramework.Repositories
{
    public class UserRepository<T, TKey> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public UserRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TKey> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            var property = typeof(T).GetProperty("Id");
            if (property == null)
                throw new InvalidOperationException("Сущность не содержит свойство Id");

            return (TKey)property.GetValue(entity)!;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
}
}