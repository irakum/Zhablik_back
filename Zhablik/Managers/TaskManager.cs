using Zhablik.Models;

namespace Zhablik.Managers;

public class TaskManager
{
    private static Dictionary<string, Assignment> tasks = new Dictionary<string, Assignment>();

    public List<Assignment> GetTasksByUsername(Guid userId)
    {
        var res = tasks.Values
            .Where(t => t.UserID == userId)
            .ToList();

        return res;
    }
    
    public Assignment CreateTask(Guid userID, string title, string description, DateTime dueDate, int level,
        bool isRepetitive=false, int repetitions=0, TimeSpan repetitiveness=default)
    {
        Assignment task = new Assignment(userID, title, description, dueDate, level);
        if (isRepetitive)
        {
            for (int i = 0; i < repetitions; i++)
            {
                CreateTask(userID, title, description, 
                    dueDate+TimeSpan.FromTicks(repetitiveness.Ticks * (i+1)), level);
            }
        }
        
        tasks[task.TaskID.ToString()] = task;
        return task;
    }

    public void UpdateTaskDescription(string taskId, string description)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].Description = description;
    }
    public void UpdateTaskTitle(string taskId, string title)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].Title = title;
    }
    public void UpdateTaskDate(string taskId, DateTime date)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].DueDate = date;
    }

    public void DeleteTask(string taskId)
    {
        if (tasks.ContainsKey(taskId))
        {
            tasks[taskId].Complete();
        }
        else
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }
    }
}