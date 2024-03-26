using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Zhablik.Managers;
using Zhablik.Models;

namespace Zhablik.api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("AllowLocalhost")]
public class TaskController : ControllerBase
{
    private readonly TaskManager _taskManager;

    public TaskController(TaskManager taskManager)
    {
        _taskManager = taskManager;
    }
    
    [HttpGet("api/tasks/{userId}")]
    public IActionResult GetTasksForUser(Guid userId)
    {
        var tasks = _taskManager.GetTasksByUsername(userId);
        return Ok(new { tasks });
    }

    [HttpPost("create")]
    public IActionResult CreateTask(Guid userID, string title, string description, DateTime dueDate, int level,
        bool isRepetitive = false, int repetitions = 0, TimeSpan repetitiveness = default)
    {
        try
        {
            var task = _taskManager.CreateTask(userID, title, description, dueDate, level, 
                isRepetitive, repetitions, repetitiveness);
            return Ok(task);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPatch("{taskId}/description")]
    public IActionResult UpdateTaskDescription(string taskId, string description)
    {
        try
        {
            _taskManager.UpdateTaskDescription(new Guid(taskId), description);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpPatch("{taskId}/title")]
    public IActionResult UpdateTaskTitle(string taskId, string title)
    {
        try
        {
            _taskManager.UpdateTaskTitle( new Guid(taskId), title);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
    
    [HttpPatch("{taskId}/date")]
    public IActionResult UpdateTaskDate(string taskId, DateTime date)
    {
        try
        {
            _taskManager.UpdateTaskDate(new Guid(taskId), date);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteTask(string taskId)
    {
        try
        {
            _taskManager.DeleteTask(new Guid(taskId));
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}