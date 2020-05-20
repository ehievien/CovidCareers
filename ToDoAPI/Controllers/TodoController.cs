using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;
using ToDoAPI.Services;
using ToDoAPI.ToDo.Entity.DbContexts;

namespace ToDoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        //private readonly ITodoService _Service;
        //private readonly TodoDbContext _context;
        //public TodoController(ITodoService todoService, TodoDbContext context)
        //{
        //    _Service = todoService ?? throw new ArgumentNullException(nameof(todoService));
        //    _context = context ?? throw new ArgumentNullException(nameof(context));
        //}
        public IConfiguration _configuration { get; }
        public TodoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        List<Todo> Todos;

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTodos()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
                {
                    Todos = connection.Query<Todo>("SELECT  * from Todo ").ToList();
                    if (Todos == null)
                    {
                        return NotFound("No todo item found");
                    }
                    return Ok(Todos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetTodo(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
                {
                    Todo todo = connection.Query<Todo>("SELECT * from Todo WHERE Id=@Id", new{Id = id}).FirstOrDefault();
                    if (todo == null)
                    {
                        return NotFound("Todo not found");
                    }
                    return Ok(todo);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
                {
                    string sqlQuery = @"DELETE FROM Todo WHERE Id = @id";
                    connection.Execute(sqlQuery, new { Id = id });
                    return StatusCode(StatusCodes.Status200OK);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private bool DoesExist(string title)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
            {
                string sqlQuery = @"SELECT * FROM Todo WHERE Title=@Title";
                var todoFromDb = connection.Query<Todo>(sqlQuery, new{Title = title}).FirstOrDefault();
                if (todoFromDb != null)
                    return true;
               else return false;
            }
        }

        [HttpPost]
        [Produces("application/json")]
        public  IActionResult AddTodo([FromBody] TodoEntity todo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }

                if (DoesExist(todo.Title))
                {
                    return BadRequest("Title Exist");
                }

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
                {
                    var result = connection.Execute(@"INSERT INTO 
                        Todo(Title, Description,Completed,DateCreated, LastDateUpdated) 
                        VALUES(@Title,@Description,@Completed,@DateCreated, @LastDateUpdated); Select Scope_Identity();",
                       new{
                           Title = todo.Title,
                        Description = todo.Description,
                        Completed = todo.Completed,
                        DateCreated = DateTime.Now,
                        LastDateUpdated = DateTime.Now });                  
                   if (result > 0)
                   return StatusCode(StatusCodes.Status201Created);
                   else
                   return StatusCode(StatusCodes.Status501NotImplemented);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id,  [FromBody]TodoEntity todo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values);
                }
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConfig")))
                {
                    Todo todoo = connection.Query<Todo>("SELECT * from Todo WHERE Id=@Id", new{Id = id}).FirstOrDefault();
                    if (todo == null){
                        return NotFound("Todo not found");}

                    connection.Execute(@"UPDATE Todo 
                    SET Title=@Title,Description=@Description,Completed=@Completed,
                    LastDateUpdated=@LastDateUpdated WHERE Id=@Id",
                   new{
                        Id = id,
                        Title = todo.Title,
                        Description = todo.Description,
                        Completed = todo.Completed,
                        LastDateUpdated = DateTime.Now,
                    });
                    return Ok(todo);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}