using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Data.Infrastructure.Interfaces;
using EntityFrameworkExtras.EF6;

namespace Data.Infrastructure
{
    /// <summary>
    /// Base class for all SQL based service classes
    /// </summary>
    /// <typeparam name="T">The domain object type</typeparam>
    /// <typeparam name="TU">The database object type</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly string CreatedOn = "CreatedOn";
        private readonly string ModifiedOn = "ModifiedOn";
        private readonly string CreatedBy = "CreatedBy";
        private readonly string IsDeleted = "IsDeleted";
        private readonly string ModifiedBy = "ModifiedBy";
        private readonly string DefaultId = "6E2B9DE4-B456-4263-A0F7-CE0432556726";
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        private DbContext dataContext;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();   
        }
   
        public T SingleOrDefault(Expression<Func<T, bool>> whereCondition)
        {
            var dbResult = dbSet.Where(whereCondition).FirstOrDefault();
            return dbResult;
        }
              
        public IEnumerable<T> GetAll()
        {
            var isDeleted = typeof(T).GetProperty(IsDeleted);
            return dbSet.AsEnumerable();
                
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).AsEnumerable();
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition);
        }

        public virtual T Insert(T entity)
        {
            entity = GetCreatedInfor(entity);
            dynamic obj = dbSet.Add(entity);

            return obj;

        }

        private bool CheckIsDetached(T entity)
        {
            var result = _unitOfWork.Db.Entry(entity).State == EntityState.Added;
            if (result)
            {
                return true;
            }
            return false;
        }
        public virtual void Update(T entity)
        {
            entity = GetUpdatedInfor(entity);

            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;


        }

        private T GetUpdatedInfor(T entity)
        {
            var modifiedOnProp = typeof(T).GetProperty(ModifiedOn);
            var modifiedByProp = typeof(T).GetProperty(ModifiedBy);
            if (modifiedOnProp != null)
            {
                modifiedOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
            if (modifiedByProp != null)
            {
                modifiedByProp.SetValue(entity,Guid.Parse(DefaultId), null);
            }
            return entity;
        }
        private T GetCreatedInfor(T entity)
        {
            var createdByProp = typeof(T).GetProperty(CreatedBy);
            var createdOnProp = typeof(T).GetProperty(CreatedOn);
            var newIdProp = typeof(T).GetProperty("Id");
            if (createdOnProp != null)
            {
                createdOnProp.SetValue(entity, DateTime.UtcNow, null);
            }
            if (createdByProp != null)
            {
                createdByProp.SetValue(entity, Guid.Parse(DefaultId), null);
            }
            if (newIdProp !=null)
            {
                if (newIdProp.PropertyType == typeof(Guid))
                {
                    var value = Guid.Parse(newIdProp.GetValue(entity, null).ToString());
                    if (value == Guid.Empty)
                    {
                        newIdProp.SetValue(entity, Guid.NewGuid(), null);
                    }
                }
            }
            return entity;
        }
        public virtual void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Attach(entity);
                _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(Expression<Func<T, bool>> whereCondition)
        {
            IEnumerable<T> entities = this.GetAll(whereCondition);
            foreach (T entity in entities)
            {
                if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }

        }
        public virtual bool HardDelete(T entity)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            return true;
        }
        public bool SoftDelete(T entity)
        {
            var result = false;
            var isDeletedProp = typeof(T).GetProperty(IsDeleted);
            if (isDeletedProp != null)
            {
                result = true;
                isDeletedProp.SetValue(entity, result);
            }
            return result;
        }

        public bool Exists(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Any(whereCondition);
        }

        public T GetById(dynamic Id)
        {
            var entity = dbSet.Find(Id);

            return entity;


        }
        
        //--------------Exra generic methods--------------------------------

        public T SingleOrDefaultOrderBy(Expression<Func<T, bool>> whereCondition, Expression<Func<T, int>> orderBy, string direction)
        {
            if (direction == "ASC")
            {
                return dbSet.Where(whereCondition).OrderBy(orderBy).FirstOrDefault();

            }
            else
            {
                return dbSet.Where(whereCondition).OrderByDescending(orderBy).FirstOrDefault();
            }
        }
        
        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            return dbSet.Where(whereCondition).Count();
        }

        public IEnumerable<T> GetPagedRecords(Expression<Func<T, bool>> whereCondition, Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
        {
            return (dbSet.Where(whereCondition).OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public IEnumerable<T> GetAllWithPagedRecords(Expression<Func<T, string>> orderBy, int pageNo, int pageSize)
        {
            return (dbSet.OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize)).AsEnumerable();
        }

        public IEnumerable<T> ExecuteStoredProcedure(string query, params object[] parameters)
        {
            
            return dbSet.SqlQuery(query, parameters);
        }   
        public IEnumerable<T> ExecuteStoredProcedure<T>(object obj)
        {
            return dataContext.Database.ExecuteStoredProcedure<T>(obj);
        }
    }
}
