﻿using System.ComponentModel.DataAnnotations;

namespace ToDoListWeb.Data
{
    public class TaskBoard : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public List<WorkTask> List { get; set; }
    }
}
