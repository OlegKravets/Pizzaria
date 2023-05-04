using Microsoft.EntityFrameworkCore;
using Pizzaria.Data;
using System.Linq.Expressions;

namespace Pizzaria.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool IsEmpty => Data is null || Data.Any();

        protected ApplicationDbContext DbContext { get; }

        protected abstract DbSet<T> Data { get; }

        public void AddEntity(T entity)
        {
            if (entity is null)
            {
                return;
            }

            Data.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate) => Data.FirstOrDefault(predicate);

        public IEnumerable<T> GetAllEntities() => Data.ToList();

        public void RemoveEntity(T entity)
        {
            if (entity is null || !Data.Contains(entity))
            {
                return;
            }

            Data.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (!entities?.Any() ?? true)
            {
                return;
            }

            Data.RemoveRange(entities);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => Data.Where(predicate);

        public bool Any(Expression<Func<T, bool>> predicate) => Data.Any(predicate);

        public async Task SaveChanges() => await DbContext.SaveChangesAsync();
    }
}
