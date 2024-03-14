//using System.Runtime.InteropServices.JavaScript;
using Zhablik.Models;

namespace Zhablik.Managers;
public class StatisticsManager 
{
    private Dictionary<string, User> _users { get; set; } = new();

    public Dictionary<string, int> GetDailyStats(string username, DateTime currentDate = default)
    {
        User user = _users[username]; 
        int tasksDone = 0;
        int tasksAssigned = 0;
        int coinsEarned = 0;

        if (currentDate == default)
        {
            currentDate = DateTime.Today;
        }
        
        foreach (var task in user.Tasks)
        {
            if (task.DueDate.Date == currentDate)
            {
                tasksAssigned++;
                
                if (task.IsComplete)
                {
                    tasksDone++;
                    coinsEarned += task.Level * 10;
                }
            }
        }

        return new Dictionary<string, int>
        {
            { "TasksDone", tasksDone },
            { "TasksAssigned", tasksAssigned },
            { "CoinsEarned", coinsEarned }
        };
    }


    public Dictionary<string, int> GetWeeklyStatistics(string username)
    {
        int tasksDone = 0;
        int tasksAssigned = 0;
        int coinsEarned = 0;
        Dictionary<string, int> daily;

        foreach (var day in DateManager.GetDaysOfWeek(DateTime.Today))
        {
            daily = GetDailyStats(username, day);
            tasksDone += daily["TasksDone"];
            tasksAssigned += daily["TasksAssigned"];
            coinsEarned += daily["coinsEarned"];
        }
        
        return new Dictionary<string, int>
        {
            { "TasksDone", tasksDone },
            { "TasksAssigned", tasksAssigned },
            { "CoinsEarned", coinsEarned }
        };
    }

    public Dictionary<string, int> GetMonthlyStatistics(string username)
    {
        int tasksDone = 0;
        int tasksAssigned = 0;
        int coinsEarned = 0;
        Dictionary<string, int> daily;

        foreach (var day in DateManager.GetDaysOfMonth(DateTime.Today))
        {
            daily = GetDailyStats(username, day);
            tasksDone += daily["TasksDone"];
            tasksAssigned += daily["TasksAssigned"];
            coinsEarned += daily["coinsEarned"];
        }
        
        return new Dictionary<string, int>
        {
            { "TasksDone", tasksDone },
            { "TasksAssigned", tasksAssigned },
            { "CoinsEarned", coinsEarned }
        };
    }
}