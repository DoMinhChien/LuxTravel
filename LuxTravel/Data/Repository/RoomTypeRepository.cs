using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Infrastructure;
using Data.Infrastructure.Interfaces;

namespace Data.Repository
{
   public class RoomTypeRepository : BaseRepository<Room_Types>
    {
        public RoomTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
