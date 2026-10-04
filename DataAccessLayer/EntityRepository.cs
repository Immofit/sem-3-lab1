using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly AppDbContext<T> _context;

        public EntityRepository(AppDbContext<T> context)
        {
            _context = context;
            _context.Database.EnsureCreated();   // создаст базу и таблицу Cars, если их нет
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var item = _context.Set<T>().Find(id);
            if (item == null) return false;
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
            return true;
        }

        public List<T> ReadAll() => _context.Set<T>().AsNoTracking().ToList();

        public T? ReadById(int id) =>
            _context.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id);

        public bool Update(T item)
        {
            _context.Set<T>().Update(item);
            return _context.SaveChanges() > 0;
        }
    }
}