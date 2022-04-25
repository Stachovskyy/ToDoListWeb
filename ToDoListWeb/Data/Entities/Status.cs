﻿namespace ToDoListWeb.Data.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<WorkTask> Tasks { get; set; }
    }
}
