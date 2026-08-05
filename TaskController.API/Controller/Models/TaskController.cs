using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.Models;

namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // In-memory task list starter data
    private static List<TaskItem> tasks = new List<TaskItem>()
    {
        new TaskItem { Id = 1, Title = "Finish Capstone", Description = "Complete the final project for class", IsCompleted = false, Priority = "High" },
        new TaskItem { Id = 2, Title = "Study C#", Description = "Review Web API notes", IsCompleted = true, Priority = "Medium" },
        new TaskItem { Id = 3, Title = "Clean Workspace", Description = "Organize desk area", IsCompleted = false, Priority = "Low" }
    };

    [HttpGet]
    public IActionResult GetAllTasks()
    {
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound($"Task with ID {id} was not found.");
        }
        return Ok(task);
    }

    [HttpPost]
    public IActionResult CreateTask(TaskItem newTask)
    {
        if (string.IsNullOrEmpty(newTask.Title))
        {
            return BadRequest("Title is required.");
        }

        int nextId = 1;
        if (tasks.Count > 0)
        {
            nextId = tasks.Max(t => t.Id) + 1;
        }

        newTask.Id = nextId;
        tasks.Add(newTask);

        return CreatedAtAction(nameof(GetTaskById), new { id = newTask.Id }, newTask);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TaskItem updatedTask)
    {
        var existingTask = tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound($"Task with ID {id} was not found.");
        }

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.IsCompleted = updatedTask.IsCompleted;
        existingTask.Priority = updatedTask.Priority;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);
        if (taskToDelete == null)
        {
            return NotFound($"Task with ID {id} was not found.");
        }

        tasks.Remove(taskToDelete);
        return NoContent();
    }
}