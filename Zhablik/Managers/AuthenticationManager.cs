using Zhablik.Models;

namespace Zhablik.Managers;

public class AuthenticationManager
{
    private Dictionary<string, User?> Users { get; set; }

    public AuthenticationManager()
    {
        Users = new Dictionary<string, User?>();
    }

    public User? Register(string username, string email, string password)
    {
        if (Users.ContainsKey(username))
        {
            Console.WriteLine("Username already exists.");
            return null;
        }

        var user = new User(username, email, password);
        Users.Add(username, user);
        return user;
    }

    public User? Login(string username, string password)
    {
        if (!Users.ContainsKey(username))
        {
            Console.WriteLine("User not found.");
            return null;
        }

        var user = Users[username];

        if (user?.Password != password)
        {
            Console.WriteLine("Incorrect password.");
            return null;
        }

        return user;
    }
}