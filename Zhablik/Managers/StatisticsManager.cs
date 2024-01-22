using System.Runtime.InteropServices.JavaScript;
using Zhablik.Models;

namespace Zhablik.Managers;
public class StatisticsManager 
{
    private Dictionary<string, User> _users { get; set; } = new();

    public static Dictionary<string, int> GetDailyStats(User user, DateTime currentDate = default)
    {
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


    public Dictionary<string, int> GetWeeklyStatistics(User user)
    {
        int tasksDone = 0;
        int tasksAssigned = 0;
        int coinsEarned = 0;
        Dictionary<string, int> daily;

        foreach (var day in DateManager.GetDaysOfWeek(DateTime.Today))
        {
            daily = GetDailyStats(user, day);
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

    public Dictionary<string, int> GetMonthlyStatistics(User user)
    {
        int tasksDone = 0;
        int tasksAssigned = 0;
        int coinsEarned = 0;
        Dictionary<string, int> daily;

        foreach (var day in DateManager.GetDaysOfMonth(DateTime.Today))
        {
            daily = GetDailyStats(user, day);
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