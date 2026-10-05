using System.Collections.Generic;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс репозитория. Описывает основные CRUD операции для любой сущности.
    /// </summary>
    /// <typeparam name="T">Тип сущности, которая хранится в репозитории.</typeparam>
    public interface IRepository<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Добавляет сущность в хранилище. После добавления у сущности заполняется Id.
        /// </summary>
        /// <param name="item">Сущность для добавления.</param>
        void Add(T item);

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>true, если сущность найдена и удалена; иначе false.</returns>
        bool Delete(int id);

        /// <summary>
        /// Возвращает все сущности из хранилища.
        /// </summary>
        /// <returns>Список всех сущностей.</returns>
        List<T> ReadAll();

        /// <summary>
        /// Ищет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null, если не найдена.</returns>
        T? ReadById(int id);

        /// <summary>
        /// Сохраняет изменения сущности в хранилище.
        /// </summary>
        /// <param name="item">Сущность с новыми данными (Id должен совпадать с существующей).</param>
        /// <returns>true, если сущность найдена и обновлена; иначе false.</returns>
        bool Update(T item);
    }
}