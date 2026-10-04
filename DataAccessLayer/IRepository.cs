using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class, IDomainObject
    {
        void Add(T item);
        void Delete(int id);
        IEnumerable<T> ReadAll();
        T? ReadById(int id);
        void Update(T item);
    }
}
