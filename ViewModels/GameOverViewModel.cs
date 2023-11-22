using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class GameOverViewModel : ObservableObject
{
    private GameManager gameManager;

    public string GameOverReason => gameManager.GameOverReason;
    private List<Player> winners;

    public GameOverViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;
        winners = gameManager.Winners;
    }
}
