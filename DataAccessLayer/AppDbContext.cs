using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Контекст базы данных для Entity Framework. Работает с любой сущностью,
    /// которая реализует IDomainObject, поэтому не привязан к конкретной сущности типа Car.
    /// </summary>
    /// <typeparam name="T">Тип сущности, для которой создаётся таблица.</typeparam>
    public class AppDbContext<T> : DbContext where T : class, IDomainObject
    {
        /// <summary>
        /// Таблица сущностей.
        /// </summary>
        public DbSet<T> Items { get; set; }

        /// <summary>
        /// Настраивает подключение к базе данных.
        /// </summary>
        /// <param name="optionsBuilder">Построитель настроек контекста.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbSettings.ConnectionString);
        }

        /// <summary>
        /// Задаёт имя таблицы: имя класса + "s" ,
        /// такое же имя использует DapperRepository.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели данных.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T>().ToTable(typeof(T).Name + "s");
        }
    }
}