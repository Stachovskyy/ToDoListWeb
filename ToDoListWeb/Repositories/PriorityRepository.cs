﻿using Microsoft.EntityFrameworkCore;

namespace ToDoListWeb.Data
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly MainContext _context;

        public PriorityRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<Priority> AddPriority(Priority Priority)
        {
            var createdpriority = await _context.Priorities.AddAsync(Priority);

            _context.SaveChanges();

            return createdpriority.Entity;

        }

        public async Task<List<Priority>> GetPrioritiesAsync()
        {
            var listOfPriorities = await _context.Priorities
                .Where(p => p.IsDeleted != true)
                .ToListAsync();

            return listOfPriorities;

        }

        public async Task<Priority> GetPriority(int priorityId)
        {
            var priority = await _context.Priorities
                .Where(p => p.Id == priorityId)
                .SingleOrDefaultAsync();

            return priority;

        }
        public async Task<Priority> GetPriorityByName(string Name)
        {
            var priority = await _context.Priorities
                .Where(p => p.Name == Name)
                .SingleOrDefaultAsync();

            return priority;
        }
        public async Task SoftDelete(int priorityId)
        {
            var priorityToDelete = await GetPriority(priorityId);

            priorityToDelete.IsDeleted = true;

            _context.SaveChanges();

        }
    }
}
