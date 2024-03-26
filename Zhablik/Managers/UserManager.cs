using System;
using System.Linq;
using Zhablik.Data;
using Zhablik.Models;

namespace Zhablik.Managers
{
    public class UserManager
    {
        private readonly AppDbContext _context;

        public UserManager(AppDbContext context)
        {
            _context = context;
        }

        public User CreateUser(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                throw new InvalidOperationException($"User {username} already exists.");
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new InvalidOperationException($"User {username} does not exist.");
            }

            return user;
        }

        public void UpdateUser(string username, User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User {username} does not exist.");
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            _context.SaveChanges();
        }

        public void DeleteUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                throw new InvalidOperationException($"User {username} does not exist.");
            }

            foreach (var task in user.Tasks.ToList())
            {
                _context.Tasks.Remove(task);
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
