using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiProje.Entities;

namespace WebApiProje.DataAccess
{
    //hangi nesneyi gönderirsek gönderelim gelen nesne IEntity tipinde new lenen bir nesne 
    //olduğu sürece işlemler yapılacak
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
