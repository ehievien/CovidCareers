using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using ToDoAPI.Models;
using ToDoAPI.ToDo.Entity.DbContexts;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }


        List<Todo> Todos = new List<Todo>
        {
            new Todo { Id = 1, Description = "Cook ", Title = "Learn How to Cook", Completed = false },
            new Todo { Id = 2, Description = "Yo-yo", Title = "Toys", Completed =true, DateCreated = DateTime.Now,LastDateUpdated = DateTime.Now },
            new Todo { Id = 3, Description = "Hammer", Title = "Hardware", Completed = false, DateCreated = DateTime.Now, LastDateUpdated = DateTime.Now }
        };
 
    [HttpGet]
        [Produces("application/json")]
        public List<Todo> GetAllTodos()
        {

            return Todos;
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateTodo([FromBody] Todo t)
        {

            try
            {
                Todos.Add(t);

            
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }
    }
}