using Microsoft.EntityFrameworkCore;
using TaskManager.Abstractions.Repositories;

namespace TaskManager.DataAccess.Repositories
{
    public class TaskRepository (AppDbContext dbContext) : ITaskRepository
    {
        public async Task<Entities.Task> AddTaskAsync(Entities.Task task)
        {
            return (await dbContext.Tasks.AddAsync(task)).Entity;
        }

        public async Task<List<Entities.Task>> GetAllTasksAsync()
        {
            return await dbContext.Tasks.OrderBy(t => t.CreatedAt).ToListAsync();
        }

        public async Task<Entities.Task?> GetTaskByIdAsync(long id)
        {
            return await dbContext.Tasks.FindAsync(id);
        }
    }
}
