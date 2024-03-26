using System;
using System.Linq;
using Zhablik.Data;
using Zhablik.Models;

namespace Zhablik.Managers
{
    public class CoinsManager
    {
        private readonly AppDbContext _context;

        public CoinsManager(AppDbContext context)
        {
            _context = context;
        }

        public void EarnCoins(User user, int taskLevel)
        {
            user.Coins += taskLevel * 10;
            _context.SaveChanges();
        }

        public void BuyFrog(string userId, FrogInfo frogInfo)
        {
            Guid id = new Guid(userId);
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }

            if (user.Coins >= frogInfo.Price)
            {
                user.Coins -= frogInfo.Price;
                user.Frogs.Add(new UserFrog { User = user, FrogInfo = frogInfo });
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Not enough coins.");
            }
        }

        public void UpgradeFrog(string userId, UserFrog userFrog)
        {
            Guid id = new Guid(userId);
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found.");
            }

            if (user.Coins >= userFrog.FrogInfo.UpgradePrice)
            {
                user.Coins -= userFrog.FrogInfo.UpgradePrice;
                userFrog.Level += 1;
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Not enough coins.");
            }
        }
    }
}