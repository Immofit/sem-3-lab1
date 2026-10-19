using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Подготавливает хранилище к работе: создаёт базу данных и таблицу, если их ещё нет.
    /// </summary>
    /// <typeparam name="T">Тип сущности, для которой создаётся таблица.</typeparam>
    public interface IDatabaseInitializer<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Создаёт базу данных и таблицу, если они не существуют.
        /// </summary>
        void Initialize();
    }
}