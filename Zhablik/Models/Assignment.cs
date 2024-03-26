namespace Zhablik.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Assignment
{
    [Key]
    public Guid TaskID { get; set; }

    [ForeignKey("User")]
    public Guid UserID { get; set; }
    public User User { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Range(1, 3)]
    public int Level { get; set; }
    public bool IsComplete { get; set; }
    public Assignment()
    {
        // Empty constructor for Entity Framework Core
    }
    public Assignment(User user, string title, string description, DateTime dueDate, int level)
    {
        TaskID = Guid.NewGuid();
        User = user;
        UserID = user.UserID;
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