using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace MicroManager.Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private static readonly string[] Tasks =
        {
            "Cook breakfast", "Just chill", "Study psychology", "Learn how to log", "Buy some food"
        };

        private readonly ILogger<TasksController> _logger;
        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public void Get()
        {
            _logger.LogInformation($"Got request at {DateTimeOffset.Now}");
            var task = LoadTask();
            _logger.LogInformation($"Got task \"{task.Message}\"");

            _logger.LogInformation("Got request at {At} for task: {@Task}",
                DateTimeOffset.Now, task);
        }

        private static Task LoadTask()
        {
            return new Task
            {
                Date = DateOnly.MinValue,
                Message = Tasks[Random.Shared.Next(0, Tasks.Length - 1)]
            };
        }
    }
}