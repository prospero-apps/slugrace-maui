using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class RaceTrackViewModel : ObservableObject
{
    private GameManager gameManager;

    public RaceTrackViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
