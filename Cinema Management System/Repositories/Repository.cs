using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema_Management_System.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        protected readonly DbSet<T> _dbset;

        public Repository(ApplicationDbContext db)
        {
            //_db = new ApplicationDbContext();
            _db = db;
            _dbset = _db.Set<T>();
        }
        public async Task CreateAsync(T Entity, CancellationToken cancellationToken = default)
        {
            await _dbset.AddAsync(Entity, cancellationToken);
        }
        public void Update(T Entity)
        {
            _dbset.Update(Entity);

        }
        public void Delete(T Entity)
        {

            _dbset.Remove(Entity);
        }
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }
        public async Task<IEnumerable<T>> GetAsync(
             Expression<Func<T, bool>>? expression = null,
             Expression<Func<T, object>>?[]? includes = null,
            bool tracked = true,
            CancellationToken cancellationToken = default

            )
        {
            var Result = _dbset.AsQueryable();
            if (expression != null)
                Result = Result.Where(expression);
            if (includes != null)
                foreach (var item in includes)
                {
                    if (item is not null)
                        Result = Result.Include(item);
                }
            if (! tracked )
                Result = Result.AsNoTracking();

            return await Result.ToListAsync(cancellationToken);
        }
        public async Task<T?> GetOneAsync(
             Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>?[]? includes = null,
            bool tracked = true,
            CancellationToken cancellationToken = default
            )
        {
            return (  await GetAsync(expression, includes, tracked, cancellationToken)).FirstOrDefault();
        }
    }
}
