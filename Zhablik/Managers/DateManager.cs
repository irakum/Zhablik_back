namespace Zhablik.Managers;

public class DateManager
{
    public static List<DateTime> GetDaysOfWeek(DateTime currentDate)
    {
        DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        List<DateTime> daysOfWeek = new List<DateTime>();

        for (DateTime date = startOfWeek; date <= endOfWeek; date = date.AddDays(1))
        {
            daysOfWeek.Add(date);
        }

        return daysOfWeek;
    }
    
    public static List<DateTime> GetDaysOfMonth(DateTime currentDate)
    {
        DateTime startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

        List<DateTime> daysOfMonth = new List<DateTime>();

        for (DateTime date = startOfMonth; date <= endOfMonth; date = date.AddDays(1))
        {
            daysOfMonth.Add(date);
        }

        return daysOfMonth;
    }
}