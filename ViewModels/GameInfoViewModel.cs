using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class GameInfoViewModel(GameManager gameManager) : ObservableObject
{
    private GameManager gameManager = gameManager;

    public EndingCondition GameEndingCondition => gameManager.EndingCondition;

    public int NumberOfRacesSet => gameManager.NumberOfRacesSet;

    public int RaceNumber
    {
        get => gameManager.RaceNumber;
        set
        {
            if (gameManager.RaceNumber != value)
            {
                gameManager.RaceNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RacesFinished));
                OnPropertyChanged(nameof(RacesToGo));
            }
        }
    } 
    
    public int RacesFinished => RaceNumber - 1;

    public int RacesToGo => NumberOfRacesSet - RacesFinished;

    public int GameTimeSet => gameManager.GameTimeSet;

    public int TimeElapsed
    {
        get => gameManager.TimeElapsed;
        set
        {
            if (gameManager.TimeElapsed != value)
            {
                gameManager.TimeElapsed = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TimeRemaining));
            }
        }
    }

    public int TimeRemaining => GameTimeSet - TimeElapsed;
}
