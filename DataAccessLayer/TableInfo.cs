using System.Collections.Generic;
using System.Reflection;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Описание таблицы для сущности: имя таблицы и список колонок (все свойства, кроме Id).
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    internal static class TableInfo<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Имя таблицы: имя класса + "s" (для Car это Cars).
        /// </summary>
        public static readonly string TableName = typeof(T).Name + "s";

        /// <summary>
        /// Свойства сущности, которые становятся колонками (все, кроме Id).
        /// </summary>
        public static readonly List<PropertyInfo> Columns = GetColumns();

        /// <summary>
        /// Собирает список колонок через рефлексию.
        /// </summary>
        /// <returns>Список свойств без Id.</returns>
        private static List<PropertyInfo> GetColumns()
        {
            var result = new List<PropertyInfo>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name != "Id")
                {
                    result.Add(property);
                }
            }
            return result;
        }
    }
}