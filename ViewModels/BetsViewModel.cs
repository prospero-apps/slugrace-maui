using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class BetsViewModel : ObservableObject
{
    private GameManager gameManager;
    private List<PlayerBetViewModel> bets;

    //public BetsViewModel(GameManager gameManager)
    //{
    //    this.gameManager = gameManager;

    //    foreach (var player in gameManager.Players)
    //    {
    //        bets.Add(new PlayerBetViewModel(gameManager, player.Id));
    //    }
    //}
}
