using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Slugrace.Views;
using System.Text;

namespace Slugrace.ViewModels;

[QueryProperty(nameof(Game), "Game")]
public partial class GameOverViewModel : ObservableObject
{
    [ObservableProperty]
    private GameViewModel game;

    [ObservableProperty]
    private string gameOverReason;

    [ObservableProperty]
    private int originalNumberOfPlayers;

    [ObservableProperty]
    private List<PlayerViewModel> winners;

    [ObservableProperty]
    public string winnerInfo;

    partial void OnGameChanged(GameViewModel value)
    {
        OriginalNumberOfPlayers = value.Players.Count;
        GameOverReason = value.GameOverReason;
        Winners = value.Winners;

        WinnerInfo = Winners.Count switch
        {
            0 => OriginalNumberOfPlayers == 1 
                 ? "There are no winners in 1-player mode." 
                 : "There is no winner!",
            1 => OriginalNumberOfPlayers == 1
                 ? $"You were playing in 1-player mode.\n"
                    + $"You started with ${Winners[0].InitialMoney}, "
                    + $"and you're leaving with ${Winners[0].CurrentMoney}."
                 : $"The winner is {Winners[0].Name}, " 
                    + $"having started with ${Winners[0].InitialMoney}, " 
                    + $"leaving with ${Winners[0].CurrentMoney}.",
            _ => $"There's a tie. The joint winners are:\n\n"
                  + $"{DisplayWinners()}"
        };
    }

    private string DisplayWinners()
    {
        StringBuilder displayMessage = new StringBuilder();

        foreach (var winner in Winners)
        {
            displayMessage.Append($"{winner.Name}, "
                + $"having started with ${winner.InitialMoney}, "
                + $"leaving with ${winner.CurrentMoney}.\n");
        }

        return displayMessage.ToString();
    }

    [RelayCommand]
    async Task RestartGame()
    {
        // Navigate to SettingsPage
        await Shell.Current.GoToAsync($"//{nameof(SettingsPage)}");
                
    }
}
