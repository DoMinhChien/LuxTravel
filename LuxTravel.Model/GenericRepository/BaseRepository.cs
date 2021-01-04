using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LuxTravel.Model.Entites;

namespace LuxTravel.Model.GenericRepository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        internal LuxTravelDBContext context;
        internal DbSet<TEntity> dbSet;
        private readonly string IsDeleted = "IsDeleted";

        public BaseRepository(LuxTravelDBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> GetMany(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            SetIdValue(entity);
            SetCreatedDateValue(entity);
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public bool SoftDelete(TEntity entity)
        {
            var result = false;
            var isDeletedProp = typeof(TEntity).GetProperty(IsDeleted);
            if (isDeletedProp != null)
            {
                result = true;
                isDeletedProp.SetValue(entity, result);
            }
            return result;
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        private void SetIdValue(TEntity entity)
        {
            var idProp = entity.GetType().GetProperty("Id");
            if (idProp == null  || idProp.PropertyType != typeof(Guid))
            {
                return;
            }

            var idVal = new Guid(idProp.GetValue(entity, null).ToString());
            if (idVal == Guid.Empty)
            {
                idProp.SetValue(entity, Guid.NewGuid());
            }


        }

        private void SetCreatedDateValue(TEntity entity)
        {
            var createdDateProp = entity.GetType().GetProperty("CreatedOn");
            if (createdDateProp != null)
            {
                createdDateProp.SetValue(entity, DateTime.Now, null);
            }

        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        //public IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate)
        //{
        //    var query = QueryHelper.GetBaseQuery<TEntity>();
        //    query = query.And(predicate);
        //    return this.context.Set<TEntity>().Where(query).AsNoTracking();
        //}

        //public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        //{
        //    var query = this.context.Set<TEntity>().AsNoTracking();
        //    foreach (Expression<Func<TEntity, object>> include in includes)
        //        query = query.Include(include);
        //    return query;
        //}

        public async Task<List<TEntity>> ExecuteSP(string query, params object[] parameters)
        {

            var result = this.dbSet.FromSqlRaw(query, parameters.ToArray()).ToList();

            return result;
        }

    }


}
