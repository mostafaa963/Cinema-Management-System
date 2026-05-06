using Cinema_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Cinema_Management_System.Repositories.IRepositories
{
    public interface IRepositorySubImage : IRepository<MoviesSubimage>
    {
        Task RemoveRang(List<MoviesSubimage> subimages);
        
    }
}
