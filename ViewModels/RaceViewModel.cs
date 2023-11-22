using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public enum RaceStatus
{
    NotYetStarted,
    GoingOn,
    Finished
}

public partial class RaceViewModel : ObservableObject
{
    private GameManager gameManager;    
    private RaceStatus raceStatus;

    public RaceStatus RaceStatus
    {
        get => raceStatus;
        set
        {
            if (raceStatus != value)
            {
                raceStatus = value;
                OnPropertyChanged();
            }
        }
    }

    public bool GameIsOver
    {
        get => gameManager.GameIsOver;
        set
        {
            if (gameManager.GameIsOver != value)
            {
                gameManager.GameIsOver = value;
                OnPropertyChanged();
            }
        }
    }

    public RaceViewModel(GameManager gameManager) 
    {
        this.gameManager = gameManager;
        RaceStatus = RaceStatus.NotYetStarted;
    }
}
