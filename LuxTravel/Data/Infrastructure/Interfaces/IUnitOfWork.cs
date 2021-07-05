using System;
using System.Data.Entity;
//using System.Data.Entity;

namespace Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        DbContext Db { get; }

        void SaveChanges();

    }
}
