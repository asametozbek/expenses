using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class EfCoreRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {

        private HomeContext _context;
        private DbSet<TEntity> table;
        public EfCoreRepository()
        {
            this._context = new HomeContext();
            table = _context.Set<TEntity>();
        }
        public EfCoreRepository(HomeContext _context)
        {
            this._context = _context;
            table = _context.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task DeleteAsync(object id)
        {
            //TEntity existing = table.Find(id);
            //table.Remove(existing);
            //table.Attach(obj);
            _context.Entry(id).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

        }

        public IEnumerable<TEntity> GetAll()
        {
            return table.ToList();
        }

        public TEntity GetById(object id)
        {
            return table.Find(id);
        }

        public IEnumerable<TEntity> GetWthLinkedTables(params string[] tableName)
        {            
            var a= table.AsQueryable();
            foreach (string s in tableName)
            {
               a = a.Include(s);
            }
            return a;         
        }

        public void Insert(TEntity obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
