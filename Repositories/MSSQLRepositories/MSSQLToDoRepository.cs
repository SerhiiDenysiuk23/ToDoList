﻿using Dapper;
using Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;
using Repositories.Models;

namespace Repositories.MSSQLRepositories
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
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO ToDo (Title, Description, CategoryId, DueDate) " +
                    "VALUES(@Title, @Description, @CategoryId, @DueDate); " +
                    "SELECT t.*, c.* FROM ToDo t LEFT JOIN Category c ON t.CategoryId = c.Id where t.Id = SCOPE_IDENTITY()";
                try
                {

                    toDo = (await connection.QueryAsync<ToDo, Category, ToDo>(
                        sqlQuery,
                        (todo, category) =>
                        {
                            todo.Category = category;
                            return todo;
                        },

                        new
                        {
                            Title = toDo.Title,
                            Description = toDo.Description,
                            CategoryId = toDo.CategoryId,
                            DueDate = toDo.DueDate
                        },
                        splitOn: "Id"
                    )).Single();
                    return toDo;
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
                    return (await connection.QueryAsync<ToDo, Category, ToDo>(
                        sqlQuery,
                        (todo, category) =>
                        {
                            todo.Category = category;
                            return todo;
                        },
                        splitOn: "Id",
                        param: new { Id = id }
                        )).Single();
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
                var sqlQuery = "SELECT t.*, c.* " +
                    "FROM ToDo t " +
                    "LEFT JOIN Category c ON t.CategoryId = c.Id " +
                    "ORDER BY " +
                    "CASE WHEN t.[Status] = 'In progress' THEN 0 ELSE 1 END, " +
                    "CASE WHEN t.[DueDate] IS NULL THEN 1 ELSE 0 END, " +
                    "t.[DueDate]";

                try
                {
                    var todo = (await connection.QueryAsync<ToDo, Category, ToDo>(
                        sqlQuery,
                        (todo, category) =>
                        {
                            todo.Category = category;
                            return todo;
                        },
                        splitOn: "Id"
                    )).ToList();
                    return todo;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get list", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<ToDo> Update(ToDo toDo)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE ToDo SET " +
                    //"Title = @Title, " +
                    //"Description = @Description, " +
                    //"CategoryId = @CategoryId, " +
                    //"DueDate = @DueDate, " +
                    "Status = @Status " +
                    "WHERE Id = @Id";
                try
                {
                    var affectedRows = await connection.ExecuteAsync(sqlQuery, new { Id = toDo.Id, Status = toDo.Status });
                    if (affectedRows > 0)
                        return toDo;
                    throw new Exception("Error to update item");
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error to get list", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
