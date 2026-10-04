using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : class, IDomainObject
    {
        void Add(T item);
        bool Delete(int id);
        List<T> ReadAll();
        T? ReadById(int id);
        bool Update(T item);
    }
}
