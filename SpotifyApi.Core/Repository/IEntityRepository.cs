using SpotifyApi.Core.Entities;
using System.Linq.Expressions;

namespace SpotifyApi.Core.Repository
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter = null);

        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        T GetLast(Expression<Func<T, bool>> filter = null, Expression<Func<T, int>> orderby = null);
        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
