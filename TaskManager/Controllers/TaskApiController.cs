using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Abstractions.Services;
using TaskManager.Dtos;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskApiController(ITaskService taskService): Controller
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await taskService.GetAllTasksAsync());
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddTask(TaskDto dto)
        {
            return Ok(await taskService.AddTaskAsync(dto));
        }

        [HttpPut("{id:long}/update")]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> UpdateTask([FromRoute] long id, [FromBody] TaskDto dto)
        {
            return Ok(await taskService.UpdateTaskAsync(id, dto));
        }
    }
}
