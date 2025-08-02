using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class UserRepository<T, TKey> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(DbSet<T> dbSet)
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(DbSet<T> dbSet, TKey id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<TKey> CreateAsync(DbContext context, DbSet<T> dbSet, T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();

        var property = typeof(T).GetProperty("Id");
        if (property == null)
            throw new InvalidOperationException("Entity has no Id property");

        return (TKey)property.GetValue(entity)!;
    }

    public async Task<bool> UpdateAsync(DbContext context, DbSet<T> dbSet, T entity)
    {
        dbSet.Update(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(DbContext context, DbSet<T> dbSet, TKey id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity == null)
            return false;

        dbSet.Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }
}
