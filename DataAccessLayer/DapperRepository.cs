using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий, который работает с базой данных через Dapper (обычные SQL-запросы).
    /// Отвечает только за CRUD. Создание базы и таблицы вынесено в IDatabaseInitializer.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private string connectionString;
        private string tableName;
        private List<string> columns;

        /// <summary>
        /// Создаёт репозиторий и просит инициализатор подготовить базу и таблицу.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        /// <param name="initializer">Инициализатор, создающий базу и таблицу.</param>
        public DapperRepository(string connectionString, IDatabaseInitializer<T> initializer)
        {
            this.connectionString = connectionString;
            tableName = TableInfo<T>.TableName;
            columns = TableInfo<T>.Columns.Select(property => property.Name).ToList();

            initializer.Initialize();
        }

        /// <summary>
        /// Добавляет сущность в таблицу и записывает в неё Id, который выдала база данных.
        /// </summary>
        /// <param name="item">Сущность для добавления.</param>
        public void Add(T item)
        {
            string names = "";
            string values = "";

            foreach (string column in columns)
            {
                if (names != "")
                {
                    names += ", ";
                    values += ", ";
                }
                names += column;
                values += "@" + column;
            }

            string sql = "INSERT INTO " + tableName + " (" + names + ") VALUES (" + values + "); " +
                "SELECT CAST(SCOPE_IDENTITY() AS int);";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                item.Id = connection.ExecuteScalar<int>(sql, item);
            }
        }

        /// <summary>
        /// Удаляет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>true, если сущность найдена и удалена; иначе false.</returns>
        public bool Delete(int id)
        {
            string sql = "DELETE FROM " + tableName + " WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int rows = connection.Execute(sql, new { Id = id });
                return rows > 0;
            }
        }

        /// <summary>
        /// Возвращает все сущности из таблицы.
        /// </summary>
        /// <returns>Список всех сущностей.</returns>
        public List<T> ReadAll()
        {
            string sql = "SELECT * FROM " + tableName;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(sql).ToList();
            }
        }

        /// <summary>
        /// Ищет сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Найденная сущность или null.</returns>
        public T? ReadById(int id)
        {
            string sql = "SELECT * FROM " + tableName + " WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<T>(sql, new { Id = id });
            }
        }

        /// <summary>
        /// Обновляет все поля сущности в таблице.
        /// </summary>
        /// <param name="item">Сущность с новыми данными.</param>
        /// <returns>true, если сущность найдена и обновлена; иначе false.</returns>
        public bool Update(T item)
        {
            string set = "";
            foreach (string column in columns)
            {
                if (set != "")
                {
                    set += ", ";
                }
                set += column + " = @" + column;
            }

            string sql = "UPDATE " + tableName + " SET " + set + " WHERE Id = @Id";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int rows = connection.Execute(sql, item);
                return rows > 0;
            }
        }
    }
}