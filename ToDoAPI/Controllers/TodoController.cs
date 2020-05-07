using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ToDoAPI.Models;
using ToDoAPI.Services;


namespace ToDoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ITodoService _Service;
        public TodoController(ITodoService todoService)
        {
            _Service = todoService ?? throw new ArgumentNullException(nameof(todoService));
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
        public TodoEntity DeleteTodo(TodoEntity todo)
        {
           _Service.DeleteTodo(todo);
            return todo;
        }


    }
}