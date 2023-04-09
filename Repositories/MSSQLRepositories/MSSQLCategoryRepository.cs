using Dapper;
using Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;
using Repositories.Models;

namespace Repositories.Repositories
{
    public class MSSQLCategoryRepository : ICategoryRepository
    {
        private string connectionString;

        public MSSQLCategoryRepository(string conn)
        {
            connectionString = conn;
        }


        public async Task<Category> Create(Category category)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Category (Name) " +
                    "VALUES(@Name); " +
                    "SELECT CAST(SCOPE_IDENTITY() as INT)";
                try
                {

                    category.Id = (await connection.QueryAsync<int>(sqlQuery, category)).Single();
                    return category;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error creating ToDo", ex);
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE Category WHERE Id = @Id";
                try
                {
                    var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = id });
                    return affectedRows > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to delete item", ex);
                }
            }
        }

        public async Task<Category> Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Category WHERE Id = @Id";
                try
                {
                    return (await connection.QueryAsync<Category>(sqlQuery, new { Id = id })).Single();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get item", ex);
                }
            }
        }

        public async Task<IEnumerable<Category>> GetList()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT * FROM Category";
                try
                {
                    return (await connection.QueryAsync<Category>(sqlQuery)).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get list", ex);
                }
            }
        }
    }
}
