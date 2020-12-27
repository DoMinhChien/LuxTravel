using System.Threading.Tasks;

namespace LuxTravel.Model.GenericRepository.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task CommitAsync();
        void Dispose();
    }
}
