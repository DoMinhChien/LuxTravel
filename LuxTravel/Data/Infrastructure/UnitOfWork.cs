using System.Data.Entity;
using Data.Infrastructure.Interfaces;

namespace Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LuxTravelEntities _dbContext;

        public UnitOfWork()
        {
            _dbContext = new LuxTravelEntities();
            //_dbContext.SaveChanges();
        }

        public DbContext Db
        {
            get
            {
                return _dbContext;
            }
        }

     
        public void Dispose()
        {
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }

}
