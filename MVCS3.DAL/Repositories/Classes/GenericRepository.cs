using Microsoft.EntityFrameworkCore;
using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models.DepartmentModel;
using MVCS3.DAL.Models.Shared;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //Add TEntity
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }
        //Update TEntity
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        //Delete TEntity
        public int Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}
