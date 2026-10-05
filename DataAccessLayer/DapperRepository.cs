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
    /// Имя таблицы = имя класса + "s" (для Car это таблица Cars).
    /// Dapper сам не создаёт ни базу, ни таблицу, поэтому репозиторий делает это сам.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private string connectionString;
        private string tableName;
        private List<string> columns;

        /// <summary>
        /// Создаёт репозиторий, определяет имя таблицы и список колонок (все свойства, кроме Id),
        /// затем создаёт базу данных и таблицу, если их ещё нет.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        public DapperRepository(string connectionString)
        {
            this.connectionString = connectionString;
            tableName = typeof(T).Name + "s";

            columns = new List<string>();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name != "Id")
                {
                    columns.Add(property.Name);
                }
            }

            EnsureDatabase();
            EnsureTable();
        }

        /// <summary>
        /// Определяет SQL-тип колонки по типу свойства в C#.
        /// </summary>
        /// <param name="type">Тип свойства.</param>
        /// <returns>Название SQL-типа.</returns>
        private string SqlType(Type type)
        {
            if (type == typeof(string))
            {
                return "NVARCHAR(MAX)";
            }
            if (type == typeof(bool))
            {
                return "BIT";
            }
            return "INT";
        }

        /// <summary>
        /// Создаёт базу данных, если она ещё не существует.
        /// Подключается к системной базе master, так как нашей базы может ещё не быть.
        /// </summary>
        private void EnsureDatabase()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string databaseName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            string sql = "IF DB_ID(N'" + databaseName + "') IS NULL CREATE DATABASE [" + databaseName + "]";

            using (var connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        /// <summary>
        /// Создаёт таблицу, если она ещё не существует.
        /// </summary>
        private void EnsureTable()
        {
            string columnsSql = "";
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.Name != "Id")
                {
                    columnsSql += ", " + property.Name + " " + SqlType(property.PropertyType);
                }
            }

            string sql = "IF OBJECT_ID(N'" + tableName + "', N'U') IS NULL " +
                "CREATE TABLE " + tableName + " (Id INT IDENTITY(1,1) PRIMARY KEY" + columnsSql + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(sql);
            }
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