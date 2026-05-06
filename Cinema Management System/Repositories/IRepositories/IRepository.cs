using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema_Management_System.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T Entity, CancellationToken cancellationToken = default);

        void Update(T Entity);


        void Delete(T Entity);

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>?[]? includes = null,
           bool tracked = true,
           CancellationToken cancellationToken = default

           );

        Task<T?> GetOneAsync(
              Expression<Func<T, bool>>? expression = null,
             Expression<Func<T, object>>?[]? includes = null,
             bool tracked = true,
             CancellationToken cancellationToken = default
             );

    }

}
