using TaskManager.Abstractions.Repositories;
using TaskManager.Abstractions.Services;
using TaskManager.Dtos;

namespace TaskManager.Services
{
    public class TaskService(
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork) : ITaskService
    {
        public async Task<Entities.Task> AddTaskAsync(TaskDto dto)
        {
            var task = new Entities.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var addedTask = await taskRepository.AddTaskAsync(task);
            await unitOfWork.SaveChangesAsync();

            return addedTask;
        }

        public async Task<List<Entities.Task>> GetAllTasksAsync()
        {
            return await taskRepository.GetAllTasksAsync();
        }

        public async Task<Entities.Task> UpdateTaskAsync(long taskId, TaskDto dto)
        {
            var task = await taskRepository.GetTaskByIdAsync(taskId);

            if (task == null)
            {
                throw new Exception();
            }

            task.Description = dto.Description;
            task.Title = dto.Title;

            await unitOfWork.SaveChangesAsync();

            return task;
        }
    }
}
