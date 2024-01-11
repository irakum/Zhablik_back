using Zhablik.Models;

namespace Zhablik.Managers;

public class UserManager
{
    private Dictionary<string, User> _users = new();

    public User CreateUser(string username, string email, string password)
    {
        if (_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} already exists.");
        }

        var user = new User(username, email, password);

        _users.Add(username, user);

        return user;
    }

    public User GetUser(string username)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }

        return _users[username];
    }

    public void UpdateUser(string username, User user)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }

        _users[username] = user;
    }

    public void DeleteUser(string username)
    {
        if (!_users.ContainsKey(username))
        {
            throw new InvalidOperationException($"User {username} does not exist.");
        }

        _users.Remove(username);
        //delete tasks accordingly
    }
}