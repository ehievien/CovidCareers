using Microsoft.AspNetCore.Mvc;
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

        public TodoEntity DeleteTodo(int id)
        {
            var todoItem = _context.Todo.Find(id);
            if (todoItem == null)
            {
                return null;
            }
            _context.Todo.Remove(todoItem);
            _context.SaveChanges();
            return todoItem;
        }

        public TodoEntity AddTodo(TodoEntity todo)
        {
            if (todo == null)
            {
                throw new ArgumentNullException(nameof(todo));
            }
            _context.Todo.Add(todo);
            _context.SaveChanges();
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
            using (var context = _context)
            {
                // Retrieve entity by id
                // Answer for question #1
                var entity = context.Todo.FirstOrDefault(item => item.Id == todo.Id);

                // Validate entity is not null
                if (entity != null)
                {
                    //make changes
                    entity.Title = todo.Title;
                    entity.Description = todo.Description;
                    entity.DateCreated = todo.DateCreated;
                    entity.LastDateUpdated = todo.LastDateUpdated;
                    entity.Completed = todo.Completed;
                    // Update entity in DbSet
                    context.Todo.Update(entity);
                    // Save changes in database
                    context.SaveChanges();
                }
            }           
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
