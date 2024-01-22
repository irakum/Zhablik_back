using Zhablik.Models;
using Zhablik.Managers;

namespace Zhablik.Managers;

public class UserManager
{
    private static Dictionary<string, User> _users = new();

    public static User CreateUser(string username, string email, string password)
    {
        if (_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} already exists.");
        }

        var user = new User(username, email, password);

        _users.Add(username, user);

        return user;
    }

    public static User GetUser(string username)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }

        return _users[username];
    }

    public static void UpdateUser(string username, User user)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }

        _users[username] = user;
    }

    public static void DeleteUser(string username)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }
        foreach (var task in _users[username].Tasks)
        {
            TaskManager.DeleteTask(task.TaskID.ToString());
        }
        
        _users.Remove(username);
    }
}