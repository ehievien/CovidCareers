using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;

namespace ToDoAPI.TodoDTO
{
    public class TodoDT
    {
        private string ConnectionString;

        public TodoDT()
        {
            ConnectionString = @"Persist security info = false; Server=Ehinomen;Database=ToDoDB;Trusted_Connection=True;MultipleActiveResultSets=true;";
        }

       

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }

        }

        public int AddTodo(TodoEntity todo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string Query = @"INSERT INTO Todo(Title, Description,Completed,DateCreated, LastDateUpdated) 
                        VALUES(@Title,@Description,@Completed,@DateCreated, @LastDateUpdated)";
                dbConnection.Open();
               return  dbConnection.Execute(Query, todo);
            }
        }

        public IEnumerable<TodoEntity> GetAllTodo()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string Query = @"SELECT * From Todo";
                dbConnection.Open();
                return dbConnection.Query<TodoEntity>(Query);
            }
        }


        public TodoEntity GetTodo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string Query = @"SELECT * From Todo where Id =@Id";
                dbConnection.Open();
                return dbConnection.Query<TodoEntity>(Query, new { Id = id }).FirstOrDefault();
            }

        }

        public int DeleteTodo(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string Query = @"Delete From Todo where Id =@Id";
                dbConnection.Open();
                return dbConnection.Execute(Query, new { Id = id });
            }
        }

        public int UpdateTodo(TodoEntity ToDo)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = @"UPDATE Todo SET Title=@Title,Description=@Description,Completed=@Completed,
                                 LastDateUpdated=@LastDateUpdated WHERE Id=@Id";
                dbConnection.Open();
               return dbConnection.Execute(sQuery, ToDo);
            }
        }

        public bool DoesExist(string title)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sqlQuery = @"SELECT * FROM Todo WHERE Title=@Title";
                dbConnection.Open();
                var todoFromDb = dbConnection.Query<Todo>(sqlQuery, new { Title = title }).FirstOrDefault();
                if (todoFromDb != null)
                    return true;
                else return false;
            }
        }
    }
}
