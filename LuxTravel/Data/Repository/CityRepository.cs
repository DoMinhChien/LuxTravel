using Data.Infrastructure;
using Data.Infrastructure.Interfaces;

namespace Data.Repository
{
    public class CityRepository: BaseRepository<City>
    {
        public CityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
