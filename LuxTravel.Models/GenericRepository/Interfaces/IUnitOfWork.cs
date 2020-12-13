using System.Threading.Tasks;

namespace LuxTravel.Models.GenericRepository.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task CommitAsync();
    }
}
