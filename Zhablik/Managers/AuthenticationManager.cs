using System;
using System.Linq;
using Zhablik.Data;
using Zhablik.Models;

namespace Zhablik.Managers
{
    public class AuthenticationManager
    {
        private readonly AppDbContext _context;

        public AuthenticationManager(AppDbContext context)
        {
            _context = context;
        }

        public User? Register(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Username already exists.");
                return null;
            }

            var user = new User(username, email, password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            if (user.Password != password)
            {
                Console.WriteLine("Incorrect password.");
                return null;
            }

            return user;
        }
    }
}