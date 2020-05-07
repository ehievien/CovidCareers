using System;
using System.Collections.Generic;
using ToDoAPI.Models;

namespace ToDoAPI.Services
{
    public interface ITodoService
    {
        //get todos

        IEnumerable<TodoEntity> GetTodo();
        IEnumerable<TodoEntity> GetTodo(int todoId);

        // delete todo
        TodoEntity DeleteTodo(TodoEntity todo);

        //add todo
        TodoEntity AddTodo(TodoEntity todo);

        //update todo
        TodoEntity UpdateTodo(TodoEntity todo);
    }
}
