using Dapper;
using IRepositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace Repositories.Repositories
{
    public class MSSQLToDoRepository : IToDoRepository
    {
        private string connectionString;

        public MSSQLToDoRepository(string conn)
        {
            connectionString = conn;
        }

        public async Task<ToDo> Create(ToDo toDo)
        {
            using(IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO ToDo (Title, Description, CategoryId, DueDate) " +
                    "VALUES(@Title, @Description, @CategoryId, @DueDate); " +
                    "SELECT t.*, c.* FROM ToDo t LEFT JOIN Category c ON t.CategoryId = c.Id where t.Id = SCOPE_IDENTITY()";
                try
                {

                    return (await connection.QueryAsync<ToDo>(sqlQuery, new{ 
                        Title = toDo.Title, 
                        Description = toDo.Description, 
                        CategoryId = toDo.Category.Id, 
                        DueDate = toDo.DueDate
                    })).Single();
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
                var sqlQuery = "DELETE ToDo WHERE Id = @Id";
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

        public async Task<ToDo> Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT t.*, c.* FROM ToDo t LEFT JOIN Category c ON t.CategoryId = c.Id where t.Id = @Id";
                try
                {
                    return (await connection.QueryAsync<ToDo>(sqlQuery, new { Id = id })).Single();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get item", ex);
                }
            }
        }

        public async Task<IEnumerable<ToDo>> GetList()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "SELECT t.*, c.* FROM ToDo t LEFT JOIN Category c ON t.CategoryId = c.Id";
                try
                {
                    return (await connection.QueryAsync<ToDo>(sqlQuery)).ToList();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get list", ex);
                }
            }
        }

        public async Task<ToDo> Update(ToDo toDo)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE ToDo SET " +
                    "Title = @Title, " +
                    "Description = @Description, " +
                    "CategoryId = @CategoryId, " +
                    "DueDate = @DueDate" +
                    "Status = @Status" +
                    "WHERE Id = @Id";
                try
                {
                    var affectedRows = await connection.ExecuteAsync(sqlQuery, toDo);
                    if (affectedRows > 0)
                        return toDo;
                    throw new Exception("Error to update item");
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get list", ex);
                }
            }
        }
    }
}
