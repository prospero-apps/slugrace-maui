using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class PlayerStatsViewModel(GameManager gameManager, int playerId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Player player = gameManager.Players.Find(p => p.Id == playerId);

    public string PlayerName => player.Name;
    public int PlayerCurrentMoney => player.CurrentMoney;
}

