using Data.Infrastructure;
using Data.Infrastructure.Interfaces;

namespace Data.Repository
{
   public class DistrictRepository : BaseRepository<District>
    {
        public DistrictRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
