using System;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace ToDoAPI.DbContexts
{
    public class TodoDbContext : DbContext
    {

        public TodoDbContext(DbContextOptions<TodoDbContext> options)
           : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Todo>().HasData(
                new Todo()
                {
                    Id = 1,
                    Title = "Learn how to cook",
                    DateCreated = new DateTime(2020, 3, 23),
                    LastDateUpdated = new DateTime(2020, 4, 23),
                    Description = "Cook",
                    Completed = true
                },
                new Todo()
                {
                    Id = 1,
                    Title = "Learn how to dance",
                    DateCreated = new DateTime(2020, 2, 17),
                    LastDateUpdated = new DateTime(2020, 3, 15),
                    Description = "Dance",
                    Completed = true
                },
                new Todo()
                {
                    Id = 1,
                    Title = "Learn how to drive",
                    DateCreated = new DateTime(2020, 3, 15),
                    LastDateUpdated = new DateTime(2020, 3, 15),
                    Description = "Drive",
                    Completed = true
                }
                );


            base.OnModelCreating(modelBuilder);
        }
    }
}
