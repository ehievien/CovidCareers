using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using ToDoAPI.Models;
using ToDoAPI.Services;
using ToDoAPI.ToDo.Entity.DbContexts;

namespace ToDoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _Service;
        private readonly TodoDbContext _context;
        public TodoController(ITodoService todoService, TodoDbContext context)
        {
            _Service = todoService ?? throw new ArgumentNullException(nameof(todoService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTodos()
        {

            var todoResult = _Service.GetTodo();
            return new JsonResult(todoResult);

        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetTodo(int id)
        {

            var todoResult = _Service.GetTodo(id);
            return new JsonResult(todoResult);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
           var todoitem = _Service.DeleteTodo(id);
            if (todoitem == null)
            {
                return NotFound();
            }else{
                return new JsonResult(todoitem);
            }
        }

        [HttpPost]
        public IActionResult AddTodo(TodoEntity todo)
        {
            _Service.AddTodo(todo);
            return CreatedAtAction(nameof(AddTodo), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodo(int id,TodoEntity todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
           
            _Service.UpdateTodo(todo);
            return CreatedAtAction(nameof(UpdateTodo), new { id = todo.Id }, todo);
        }


    }
}