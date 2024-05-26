using TaskManager.Abstractions.Repositories;
using TaskManager.Abstractions.Services;
using TaskManager.Dtos;

namespace TaskManager.Services
{
    public class TaskService(
        ITaskRepository taskRepository,
        IUnitOfWork unitOfWork,
        ILogger<TaskService> logger) : ITaskService
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

            logger.LogInformation(@"Задача с ID='{id}' была добавлена в базу данных. Время добавления: '{CreatedAt}'", task.Id, task.CreatedAt);

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
                logger.LogError("Была произведена попытка обновить задачу с Id='{Id}'. Однако задача с таким Id не существует", taskId);
                throw new Exception();
            }

            logger.LogInformation(@"Задача с ID='{id}' была обновлена. Старые значения: Description='{oldDescription}', Title='{oldTitle}'.
                Новые значения: Description='{newDescription}', Title='{newTitle}' .Время обновления: '{UpdatedAt}'",
                task.Id, task.Description, task.Title, dto.Description, dto.Title, DateTimeOffset.Now);

            task.Description = dto.Description;
            task.Title = dto.Title;

            await unitOfWork.SaveChangesAsync();

            return task;
        }
    }
}
