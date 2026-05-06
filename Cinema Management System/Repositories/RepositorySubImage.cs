using Cinema_Management_System.DataAccess;
using Cinema_Management_System.Models;
using Cinema_Management_System.Repositories.IRepositories;

namespace Cinema_Management_System.Repositories
{
    public class RepositorySubImage : Repository<MoviesSubimage>, IRepositorySubImage
    {
        public RepositorySubImage(ApplicationDbContext db) :base(db)
        {
            
        }
        public async Task RemoveRang(List<MoviesSubimage> subimages)
        {
            _dbset.RemoveRange(subimages);
        }
    }
}
