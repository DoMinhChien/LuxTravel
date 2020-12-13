using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LuxTravel.Models.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Models.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;


        }

        public DbContext Context => _context;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}