using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Test.Data.Entities;

namespace Test.Data.Repositories
{
    public interface IBaseRepo<T>
    {
        IQueryable<T> Base();
        IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression);
        Task<Guid> Add(T entity);
        Task AddRange(IEnumerable<T> entity);
        Task Save();
        Task<T> GetById(Guid id);
        Task Delete(T entity);
        Task DeleteRange(IEnumerable<T> entities);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateRange(IEnumerable<T> entity);
    }

    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        private readonly AppDbContext _contextBase;
        private readonly DbSet<T> _repo;

        public BaseRepo(AppDbContext context)
        {
            _contextBase = context;
            _repo = _contextBase.Set<T>();
        }

        public IQueryable<T> Base() => _repo.AsQueryable();

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression)
        {
            return _repo.Where(expression);
        }

        public async Task<Guid> Add(T entity)
        {
            await _repo.AddAsync(entity);
            await Save();
            return entity.Id;
        }

        public async Task AddRange(IEnumerable<T> entity)
        {
            await _repo.AddRangeAsync(entity);
            await Save();
        }

        public async Task Save()
        {
            await _contextBase.SaveChangesAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _repo.FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _repo.Remove(entity);
            await Save();
        }

        public async Task RemoveRange(IEnumerable<T> entities)
        {
            _repo.RemoveRange(entities);
            await Save();
        }

        public async Task Delete(T entity)
        {
            entity.IsDeleted = true;
            await Save();
        }

        public async Task DeleteRange(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                item.IsDeleted = true;
            }
            await Save();
        }

        public async Task Update(T entity)
        {
            _repo.Update(entity);
            entity.UpdateDate = DateTime.Now;
            _contextBase.Entry(entity).Property(x => x.CreateDate).IsModified = false;
            await Save();
        }

        public async Task UpdateRange(IEnumerable<T> entity)
        {
            _repo.UpdateRange(entity);
            foreach (var item in entity)
            {
                item.UpdateDate = DateTime.Now;
            }
            await Save();
        }

    }
}