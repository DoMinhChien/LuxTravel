using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using CommonFunctionality.Helper;
using LuxTravel.Model.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace LuxTravel.Model.GenericRepository
{

    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
           where TEntity : class
           where TContext : DbContext
    {
        private readonly TContext context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public BaseRepository(TContext context, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.context = context;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            SetIdValue(entity);
            SetCreatedDateValue(entity);
            context.Set<TEntity>().Add(entity);
            _unitOfWork.SaveChanges();
            return entity;
        }

        private void SetCreatedDateValue(TEntity entity)
        {
            var createdDateProp = entity.GetType().GetProperty("CreatedOn");
            if (createdDateProp != null)
            {
                createdDateProp.SetValue(entity, DateTime.Now, null);
            }

        }
        private void SetIdValue(TEntity entity)
        {
            var idProp = entity.GetType().GetProperty("Id");
            if (idProp.PropertyType != typeof(Guid))
            {
                return;
            }

            var idVal = new Guid(idProp.GetValue(entity, null).ToString());
            if (idVal == Guid.Empty)
            {
                idProp.SetValue(entity, Guid.NewGuid());
            }


        }
        public async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {

            //TO DO handle connection
            
            return await context.Set<TEntity>().ToListAsync();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
                
            var query = this.context.Set<TEntity>().AsNoTracking();
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);
            return query;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
        {
            var query = QueryHelper.GetBaseQuery<TEntity>();
            query = query.And(predicate);
            return this.context.Set<TEntity>().Where(query).AsNoTracking();
        }
        public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.context.Set<TEntity>()
                .Where(predicate).AsNoTracking();
            foreach (Expression<Func<TEntity, object>> include in includes)
                query = query.Include(include);

            return query;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
