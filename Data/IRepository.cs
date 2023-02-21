using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWthLinkedTables(params string[] tableName);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);

        Task DeleteAsync(object id);
        void Save();
    }
}
