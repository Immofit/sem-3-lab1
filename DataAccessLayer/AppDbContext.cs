using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    public class AppDbContext<T> : DbContext where T : class, IDomainObject
    {
        public DbSet<T> Items => Set<T>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(DbSettings.ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.Entity<T>().ToTable(typeof(T).Name + "s");   // таблица Cars
    }
}