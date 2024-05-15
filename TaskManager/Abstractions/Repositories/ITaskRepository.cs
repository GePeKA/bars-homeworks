using TaskManager.Dtos;

namespace TaskManager.Abstractions.Repositories
{
    public interface ITaskRepository
    {
        Task<Entities.Task> AddTaskAsync(Entities.Task task);
        Task<List<Entities.Task>> GetAllTasksAsync();
        Task<Entities.Task?> GetTaskByIdAsync(long id);
    }
}
