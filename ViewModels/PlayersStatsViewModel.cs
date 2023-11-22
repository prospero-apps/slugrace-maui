using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class PlayersStatsViewModel : ObservableObject
{
    private GameManager gameManager;
    private List<PlayerStatsViewModel> players;

    public PlayersStatsViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;

        foreach (var player in gameManager.Players)
        {
            players.Add(new PlayerStatsViewModel(gameManager, player.Id));
        }
    }
}
