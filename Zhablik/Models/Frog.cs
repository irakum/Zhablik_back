namespace Zhablik.Models;

public class Frog
{
    public string Title { get; private set; }
    public int Level { get; set; }
    public string Description { get; private set; }

    public Frog(string title, string description)
    {
        Title = title;
        Level = 0;
        Description = description;
    }
}