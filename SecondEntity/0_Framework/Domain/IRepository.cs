using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey,T> where T:class //Tkey is type of Id & T is Type of Entities
    {
        T Get(TKey id);
        List<T> Get();
        void Create(T entity);
        bool Exists(Expression<Func<T,bool>>expression);
        void SaveChanges();
    }
}
