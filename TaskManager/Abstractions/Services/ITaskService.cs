using TaskManager.Dtos;

namespace TaskManager.Abstractions.Services
{
    public interface ITaskService
    {
        Task<List<Entities.Task>> GetAllTasksAsync();
        Task<Entities.Task> AddTaskAsync(TaskDto dto);
        Task<Entities.Task> UpdateTaskAsync(long taskId, TaskDto dto);
    }
}
