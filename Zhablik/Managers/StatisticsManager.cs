using System;
using System.Collections.Generic;
using System.Linq;
using Zhablik.Data;
using Zhablik.Models;

namespace Zhablik.Managers
{
    public class StatisticsManager
    {
        private readonly AppDbContext _context;

        public StatisticsManager(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, int> GetDailyStats(string username, DateTime currentDate = default)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new InvalidOperationException($"User {username} does not exist.");
            }

            if (currentDate == default)
            {
                currentDate = DateTime.Today;
            }

            var tasksDone = 0;
            var tasksAssigned = 0;
            var coinsEarned = 0;

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
            var tasksDone = 0;
            var tasksAssigned = 0;
            var coinsEarned = 0;

            foreach (var day in DateManager.GetDaysOfWeek(DateTime.Today))
            {
                var daily = GetDailyStats(username, day);
                tasksDone += daily["TasksDone"];
                tasksAssigned += daily["TasksAssigned"];
                coinsEarned += daily["CoinsEarned"];
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
            var tasksDone = 0;
            var tasksAssigned = 0;
            var coinsEarned = 0;

            foreach (var day in DateManager.GetDaysOfMonth(DateTime.Today))
            {
                var daily = GetDailyStats(username, day);
                tasksDone += daily["TasksDone"];
                tasksAssigned += daily["TasksAssigned"];
                coinsEarned += daily["CoinsEarned"];
            }

            return new Dictionary<string, int>
            {
                { "TasksDone", tasksDone },
                { "TasksAssigned", tasksAssigned },
                { "CoinsEarned", coinsEarned }
            };
        }
    }
}
