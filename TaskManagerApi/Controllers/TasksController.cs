using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;


namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private static List<TaskModel> tasks = new List<TaskModel>();
        private static int nextId = 1;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            task.Id = nextId++;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskModel updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.Status = updatedTask.Status;

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
