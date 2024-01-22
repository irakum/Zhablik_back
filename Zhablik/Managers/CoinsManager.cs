using Zhablik.Models;

namespace Zhablik.Managers;

public class CoinsManager
{
    private Dictionary<string, User> _users { get; set; } = new();

    public void EarnCoins(User user, int taskLevel)
    {
        user.Coins += taskLevel * 10;
    }

    public void BuyFrog(User user, Frog frog)
    {
        if (user.Coins >= frog.Price)
        {
            user.Coins -= frog.Price;
            user.Frogs.Add(frog);
        }
        else
        {
            throw new InvalidOperationException("Not enough coins.");
        }
    }

    public void UpgradeFrog(User user, Frog frog)
    {
        if (user.Coins >= frog.UpdatePrice)
        {
            user.Coins -= frog.UpdatePrice;
            frog.Level += 1;
        }
        else
        {
            throw new InvalidOperationException("Not enough coins.");
        }
    }
}