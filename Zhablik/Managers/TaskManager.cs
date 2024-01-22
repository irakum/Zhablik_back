using Zhablik.Models;

namespace Zhablik.Managers;

public class TaskManager
{
    private static Dictionary<string, Assignment> tasks = new Dictionary<string, Assignment>();
    
    public static void CreateTask(Guid userID, string title, string description, DateTime dueDate, int level,
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
    }

    public static void UpdateTaskDescription(string taskId, string description)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].Description = description;
    }
    public static void UpdateTaskTitle(string taskId, string title)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].Title = title;
    }
    public static void UpdateTaskDate(string taskId, DateTime date)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].DueDate = date;
    }

    public static void UpdateTaskLevel(string taskId, int level)
    {
        if (!tasks.ContainsKey(taskId))
        {
            throw new InvalidOperationException("This task doesn't exist.");
        }

        tasks[taskId].Level = level;
    }

    public static void DeleteTask(string taskId)
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