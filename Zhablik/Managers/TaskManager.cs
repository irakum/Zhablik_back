using Zhablik.Models;

namespace Zhablik.Managers;

public class TaskManager
{
    private Dictionary<string, Assignment> tasks = new Dictionary<string, Assignment>();
    
    public Assignment CreateTask(Guid userID, string title, string description, DateTime dueDate, int level)
    {
        Assignment task = new Assignment(userID, title, description, dueDate, level);
        tasks[task.TaskID.ToString()] = task;
        return task;
    }

    public void UpdateTask(string taskId)
    {
        if (tasks.ContainsKey(taskId))
        {
            tasks[taskId].Complete();
        }
        else
        {
            //throw exception
        }
    }

    public void DeleteTask(string taskId, string user)
    {
        if (tasks.ContainsKey(taskId))
        {
            tasks[taskId].Complete();
        }
        else
        {
            //throw exception
        }
    }
}