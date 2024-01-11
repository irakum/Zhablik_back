using Zhablik.Models;

namespace Zhablik.Managers;

public class StatisticsManager
{
    private Dictionary<string, User> _users { get; set; } = new();

    public Dictionary<string, object> GetDailyStatistics(User user)
    {
        return new Dictionary<string, object>
        {
            { "TasksDone", user.TasksDone },
            { "TasksAssigned", user.TasksAssigned },
            { "CoinsEarned", user.CoinsEarned }
        };
    }

    public Dictionary<string, object> GetWeeklyStatistics(User user)
    {
        return new Dictionary<string, object>
        {
            { "TasksDone", user.TasksDone * 7 },
            { "TasksAssigned", user.TasksAssigned * 7 },
            { "CoinsEarned", user.CoinsEarned * 7 }
        };
    }

    public Dictionary<string, object> GetMonthlyStatistics(User user)
    {
        return new Dictionary<string, object>
        {
            { "TasksDone", user.TasksDone * 30 },
            { "TasksAssigned", user.TasksAssigned * 30 },
            { "CoinsEarned", user.CoinsEarned * 30 }
        };
    }
}