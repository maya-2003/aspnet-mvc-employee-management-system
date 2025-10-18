using Microsoft.EntityFrameworkCore;
using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models.DepartmentModel;
using MVCS3.DAL.Models.Shared;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext): IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public TEntity? GetById(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);
            return entity;

        }
        //Get All TEntity
        public IEnumerable<TEntity> GetAll(bool withTracking = false)
        {
            if (withTracking) return _dbContext.Set<TEntity>().ToList();
            else return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<TEntity, TResult>> selector)
        {
            
                return _dbContext.Set<TEntity>()
                .Where(entity => entity.IsDeleted == false)
                .Select(selector).ToList();
        }

        //public IEnumerable<TEntity> GetAll(bool withTracking = false)
        //{
        //    if (withTracking) return _dbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).ToList();
        //    else return _dbContext.Set<TEntity>().Where(entity => entity.IsDeleted == false).AsNoTracking().ToList();
        //}
        //Add TEntity
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
           // return _dbContext.SaveChanges();
        }
        //Update TEntity
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            //return _dbContext.SaveChanges();
        }

        //Delete TEntity
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            //return _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(predicate).ToList();
                
        }

        //public IEnumerable<TEntity> GetIEnumerable()
        //{
        //    return _dbContext.Set<TEntity>();
        //}

        //public IQueryable<TEntity> GetIQueryable()
        //{
        //    return _dbContext.Set<TEntity>();
        //}


    }
}
