namespace Zhablik.Models;

public class Assignment
{
    public Guid TaskID { get; private set; }
    public Guid UserID { get; private set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int Level { get; set; }
    public bool IsComplete { get; set; }
    public Assignment(Guid userID, string title, string description, DateTime dueDate, int level)
    {
        TaskID = Guid.NewGuid();
        UserID = userID;
        Title = title;
        Description = description;
        DueDate = dueDate;
        Level = level;
        IsComplete = false;
    }

    public void Complete()
    {
        IsComplete = true;
    }
}