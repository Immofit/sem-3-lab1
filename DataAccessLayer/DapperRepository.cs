using Dapper;
using Microsoft.Data.SqlClient;
using Model;

namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        private readonly string _connectionString;
        private readonly string _table = typeof(T).Name + "s";
        private readonly string[] _props = typeof(T).GetProperties()
            .Where(p => p.Name != "Id").Select(p => p.Name).ToArray();

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
            EnsureTable();
        }

        private SqlConnection Open()
        {
            var c = new SqlConnection(_connectionString);
            c.Open();
            return c;
        }

        private static string SqlType(Type t)
        {
            if (t == typeof(string)) return "NVARCHAR(MAX)";
            if (t == typeof(bool)) return "BIT";
            return "INT";
        }

        // Dapper сам не создаёт ни базу, ни таблицу, поэтому делаем это вручную
        private void EnsureTable()
        {
            // 1. база (подключаемся к master, потому что нашей базы может ещё не быть)
            var builder = new SqlConnectionStringBuilder(_connectionString);
            var dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            using (var master = new SqlConnection(builder.ConnectionString))
            {
                master.Execute($"IF DB_ID(N'{dbName}') IS NULL CREATE DATABASE [{dbName}]");
            }

            // 2. таблица
            var columns = typeof(T).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => $"{p.Name} {SqlType(p.PropertyType)}");

            var sql = $"IF OBJECT_ID(N'{_table}', N'U') IS NULL " +
                      $"CREATE TABLE {_table} (Id INT IDENTITY(1,1) PRIMARY KEY, {string.Join(", ", columns)})";

            using var db = Open();
            db.Execute(sql);
        }

        public void Add(T item)
        {
            var cols = string.Join(", ", _props);
            var vals = string.Join(", ", _props.Select(p => "@" + p));
            var sql = $"INSERT INTO {_table} ({cols}) VALUES ({vals}); SELECT CAST(SCOPE_IDENTITY() AS int);";

            using var db = Open();
            item.Id = db.ExecuteScalar<int>(sql, item);
        }

        public bool Delete(int id)
        {
            using var db = Open();
            return db.Execute($"DELETE FROM {_table} WHERE Id = @id", new { id }) > 0;
        }

        public List<T> ReadAll()
        {
            using var db = Open();
            return db.Query<T>($"SELECT * FROM {_table}").ToList();
        }

        public T? ReadById(int id)
        {
            using var db = Open();
            return db.QueryFirstOrDefault<T>($"SELECT * FROM {_table} WHERE Id = @id", new { id });
        }

        public bool Update(T item)
        {
            var set = string.Join(", ", _props.Select(p => $"{p} = @{p}"));
            using var db = Open();
            return db.Execute($"UPDATE {_table} SET {set} WHERE Id = @Id", item) > 0;
        }
    }
}