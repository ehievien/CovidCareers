using System;
using System.Collections.Generic;
using System.Linq;
using ToDoAPI.Models;
using ToDoAPI.ToDo.Entity.DbContexts;

namespace ToDoAPI.Services
{
    public class TodoService :ITodoService , IDisposable
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<TodoEntity> GetTodo()
        {
           
            return _context.Todo
                        .OrderBy(c => c.Title).ToList();
        }
        public IEnumerable<TodoEntity> GetTodo(int todoId)
        {
            if (todoId == 0)
            {
                throw new ArgumentNullException(nameof(todoId));
            }

            return _context.Todo
                        .Where(c => c.Id == todoId)
                        .OrderBy(c => c.Title).ToList();
        }

        public TodoEntity DeleteTodo(TodoEntity todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            _context.Todo.Remove(todo);
            _context.SaveChanges();

            return todo;
        }

        public TodoEntity AddTodo(TodoEntity todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            if (todo.Id == 0)
            {
                throw new ArgumentNullException(nameof(todo.Id));
            }
            _context.Todo.Add(todo);
            return todo;
        }

        public TodoEntity UpdateTodo(TodoEntity todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }

            if (todo.Id == 0)
            {
                throw new ArgumentNullException(nameof(todo.Id));
            }
            _context.Todo.Update(todo);
            _context.SaveChanges();
            return todo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

    }
}
