using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAPI.Models;
using ToDoAPI.Services;
using ToDoAPI.ToDo.Entity.DbContexts;
using ToDoAPI.TodoDTO;

namespace ToDoAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        
        private readonly TodoDT dto;
        private ILogger<TodoController> _logger;
        public TodoController( ILogger<TodoController> logger)
        {           
            _logger = logger;
            dto = new TodoDT();              
        }


        IEnumerable<TodoEntity> Todos;

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTodos()
        {
            try
            {
                _logger.LogInformation($"about to get all todos");
                Todos = dto.GetAllTodo();
                return Ok (Todos);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"exception ---"+ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetTodo(int id)
        {
            try
            {
                _logger.LogInformation($"about to get todos with id --{id}");
                return Ok(dto.GetTodo(id));                
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"exception ---" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            try
            {
                _logger.LogInformation($"about to delete todo with id --{id}");
                int result = dto.DeleteTodo(id);
                if (result > 0)
                    return StatusCode(StatusCodes.Status200OK);
                else
                    return StatusCode(StatusCodes.Status501NotImplemented);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"exception ---" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

       

        [HttpPost]
        [Produces("application/json")]
        public  IActionResult AddTodo([FromBody] TodoEntity todo)
        {
            try
            {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.Values);}
                if (dto.DoesExist(todo.Title)){
                    return BadRequest("Title Exist");}
                _logger.LogInformation($"about to create todo with title --{todo.Title}");
                int result = dto.AddTodo(todo);              
                   if (result > 0)
                   return StatusCode(StatusCodes.Status201Created);
                   else
                   return StatusCode(StatusCodes.Status501NotImplemented);                
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"exception ---" + ex);
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
                _logger.LogInformation($"about to update todo with id --{todo.Id}");
                int result = dto.UpdateTodo(todo);
                if (result > 0)
                    return StatusCode(StatusCodes.Status200OK);
                else
                    return StatusCode(StatusCodes.Status501NotImplemented);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"exception ---" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}