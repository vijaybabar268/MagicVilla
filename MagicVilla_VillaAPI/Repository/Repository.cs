using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);
                        
            if(includeProperties != null)
            {
                foreach(var includeProd in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProd);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null,
            int pageSize = 3, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageNumber = 100;
                }
                // 5*(1-1).take(5) --> skip0.take(5)                
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }

            if (includeProperties != null)
            {
                foreach (var includeProd in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProd);
                }
            }

            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
