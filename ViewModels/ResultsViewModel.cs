using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class ResultsViewModel : ObservableObject
{
    private GameManager gameManager;
    private List<PlayerResultViewModel> results;

    public ResultsViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;

        foreach (var player in gameManager.Players)
        {
            results.Add(new PlayerResultViewModel(gameManager, player.Id));
        }
    }
}
