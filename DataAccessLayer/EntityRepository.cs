using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий, который работает с базой данных через Entity Framework.
    /// Контекст живёт всё время работы приложения (Singleton), поэтому чтение идёт без отслеживания,
    /// а после каждой записи трекер очищается: так поведение совпадает с DapperRepository.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class EntityRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private AppDbContext<T> context;

        /// <summary>
        /// Создаёт репозиторий для указанного контекста. Если базы данных
        /// или таблицы ещё нет, они будут созданы.
        /// </summary>
        /// <param name="context">Контекст базы данных.</param>
        public EntityRepository(AppDbContext<T> context)
        {
            this.context = context;
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Добавляет сущность в таблицу и сохраняет изменения. После добавления у сущности заполняется Id.
        /// </summary>
        /// <param name="item">Сущность для добавления.</param>
        public void Add(T item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>true, если сущность найдена и удалена; иначе false.</returns>
        public bool Delete(int id)
        {
            T? item = context.Items.Find(id);
            if (item == null)
            {
                return false;
            }

            context.Items.Remove(item);
            context.SaveChanges();
            context.ChangeTracker.Clear();
            return true;
        }

        /// <summary>
        /// Возвращает все сущности из таблицы.
        /// </summary>
        /// <returns>Список всех сущностей.</returns>
        public List<T> ReadAll()
        {
            return context.Items.AsNoTracking().ToList();
        }

        /// <summary>
        /// Ищет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        public T? ReadById(int id)
        {
            return context.Items.AsNoTracking().FirstOrDefault(entity => entity.Id == id);
        }

        /// <summary>
        /// Сохраняет изменения сущности в таблице.
        /// </summary>
        /// <param name="item">Сущность с новыми данными.</param>
        /// <returns>true, если сущность найдена и обновлена; иначе false.</returns>
        public bool Update(T item)
        {
            if (!context.Items.Any(entity => entity.Id == item.Id))
            {
                return false;
            }

            context.Items.Update(item);
            int changedRows = context.SaveChanges();
            context.ChangeTracker.Clear();
            return changedRows > 0;
        }
    }
}