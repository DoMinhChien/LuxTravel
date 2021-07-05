using Data.Infrastructure;
using Data.Infrastructure.Interfaces;

namespace Data.Repository
{
  public  class RoomRepository : BaseRepository<Room>
    {
        public RoomRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
