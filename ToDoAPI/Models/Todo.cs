using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class Todo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastDateUpdated { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }

}
