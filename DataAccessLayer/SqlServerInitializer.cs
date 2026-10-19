using System;
using System.Collections.Generic;
using System.Reflection;
using Dapper;
using Microsoft.Data.SqlClient;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Создаёт базу данных SQL Server и таблицу для сущности, если их ещё нет.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public class SqlServerInitializer<T> : IDatabaseInitializer<T> where T : class, IDomainObject
    {
        /// <summary>
        /// Соответствие типов C# и типов SQL. Чтобы поддержать новый тип, достаточно добавить строку.
        /// </summary>
        private static readonly Dictionary<Type, string> sqlTypes = new Dictionary<Type, string>
        {
            { typeof(string), "NVARCHAR(MAX)" },
            { typeof(bool), "BIT" },
            { typeof(int), "INT" },
            { typeof(long), "BIGINT" },
            { typeof(double), "FLOAT" },
            { typeof(decimal), "DECIMAL(18,2)" },
            { typeof(DateTime), "DATETIME2" }
        };

        private string connectionString;

        /// <summary>
        /// Создаёт инициализатор для указанной строки подключения.
        /// </summary>
        /// <param name="connectionString">Строка подключения к базе данных.</param>
        public SqlServerInitializer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Создаёт базу данных и таблицу, если они не существуют.
        /// </summary>
        public void Initialize()
        {
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
            if (sqlTypes.TryGetValue(type, out string? sqlType))
            {
                return sqlType;
            }
            throw new NotSupportedException("Тип " + type.Name + " не поддерживается. Добавьте его в словарь sqlTypes.");
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
            foreach (PropertyInfo property in TableInfo<T>.Columns)
            {
                columnsSql += ", " + property.Name + " " + SqlType(property.PropertyType);
            }

            string tableName = TableInfo<T>.TableName;
            string sql = "IF OBJECT_ID(N'" + tableName + "', N'U') IS NULL " +
                "CREATE TABLE " + tableName + " (Id INT IDENTITY(1,1) PRIMARY KEY" + columnsSql + ")";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }
    }
}