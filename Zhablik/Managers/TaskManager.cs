using Zhablik.Data;
using Zhablik.Models;

namespace Zhablik.Managers;

public class TaskManager
{
    private readonly AppDbContext _context;

    public TaskManager(AppDbContext context)
    {
        _context = context;
    }

    public List<Assignment> GetTasksByUsername(Guid userId)
    {
        var res = _context.Tasks
            .Where(t => t.UserID == userId)
            .ToList();

        return res;
    }
    
    public Assignment CreateTask(Guid userID, string title, string description, DateTime dueDate, int level,
        bool isRepetitive=false, int repetitions=0, TimeSpan repetitiveness=default)
    {
        var task = new Assignment
        {
            UserID = userID,
            Title = title,
            Description = description,
            DueDate = dueDate,
            Level = level,
            IsComplete = false
        };

        _context.Tasks.Add(task);
        _context.SaveChanges();

        if (isRepetitive)
        {
            for (int i = 0; i < repetitions; i++)
            {
                CreateTask(userID, title, description, 
                    dueDate + TimeSpan.FromTicks(repetitiveness.Ticks * (i + 1)), level);
            }
        }

        return task;
    }

    public void UpdateTaskDescription(Guid taskId, string description)
    {
        var task = _context.Tasks.Find(taskId);
        if (task == null)
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        task.Description = description;
        _context.SaveChanges();
    }
    public void UpdateTaskTitle(Guid taskId, string title)
    {
        var task = _context.Tasks.Find(taskId);
        if (task == null)
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        task.Title = title;
        _context.SaveChanges();
    }

    public void UpdateTaskDate(Guid taskId, DateTime date)
    {
        var task = _context.Tasks.Find(taskId);
        if (task == null)
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        task.DueDate = date;
        _context.SaveChanges();
    }

    public void DeleteTask(Guid taskId)
    {
        var task = _context.Tasks.Find(taskId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
        else
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }
    }
}