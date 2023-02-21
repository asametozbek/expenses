using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    //public class EfCoreManager :EfCoreRepository<T,>
    //{
    //}
    public class EfManager<TEntity, TContext> : IRepositoryService<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        HomeContext db = new HomeContext();
        DbSet<TEntity> _object;
        //EfCoreRepository<TEntity,TContext> _efCoreRepository;
        //public EfManager(EfCoreRepository<TEntity, TContext> efCoreRepository)
        //{
        //    _efCoreRepository = efCoreRepository;
        //    _object = db.Set<TEntity>();
        //}

        //public Task<TEntity> Add(TEntity entity)
        //{
        //    _efCoreRepository.Add(entity);
        //     db.SaveChanges();
        //    return 1;
        //}

        public Task<TEntity> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

}
