using System.Linq.Expressions;

namespace Pizzaria.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        bool IsEmpty { get; }

        IEnumerable<T> GetAllEntities();

        void AddEntity(T entity);

        void RemoveEntity(T entity);

        void RemoveRange(IEnumerable<T> entities);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        bool Any(Expression<Func<T, bool>> predicate);

        Task SaveChanges();
    }
}
