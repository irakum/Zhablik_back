namespace Zhablik.Models;

public class User
{
    public Guid UserID { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public bool Authenticated { get; set; }
    public int Coins { get; set; }
    public List<Assignment> Tasks { get; set; }
    public List<Frog> Frogs { get; set; }

    public User(string username, string email, string password)
    {
        UserID = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
        Authenticated = false;
        Coins = 0;
        Tasks = new List<Assignment>();
        Frogs = new List<Frog>();
    }

    public bool Authenticate(string username, string password)
    {
        if (this.Username == username && this.Password == password)
        {
            Authenticated = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}