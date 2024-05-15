using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaskManager.Abstractions.Services;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TaskController(ITaskService taskService): Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            var allTasks = await taskService.GetAllTasksAsync();
            var taskViewModel = new TaskViewModel { Tasks = allTasks };

            return View(taskViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
