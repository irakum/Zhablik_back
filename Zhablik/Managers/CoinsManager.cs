using Zhablik.Models;

namespace Zhablik.Managers;

public class CoinsManager
{
    private Dictionary<string, User> Users { get; set; } = new();
    private Dictionary<string, FrogInfo> Frogs { get; set; } = new();

    public void EarnCoins(User user, int taskLevel)
    {
        user.Coins += taskLevel * 10;
    }

    public void BuyFrog(int userId, FrogInfo frogInfo)
    {
        User user = Users[userId.ToString()];
        if (user.Coins >= frogInfo.Price)
        {
            user.Coins -= frogInfo.Price;
            user.Frogs.Add(new UserFrog(user, frogInfo));
        }
        else
        {
            throw new InvalidOperationException("Not enough coins.");
        }
    }

    public void UpgradeFrog(User user, UserFrog userFrog)
    {
        if (user.Coins >= userFrog.FrogInfo.UpgradePrice)
        {
            user.Coins -= userFrog.FrogInfo.UpgradePrice;
            userFrog.Level += 1;
        }
        else
        {
            throw new InvalidOperationException("Not enough coins.");
        }
    }
}