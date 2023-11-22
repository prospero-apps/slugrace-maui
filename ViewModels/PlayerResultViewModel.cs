using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class PlayerResultViewModel(GameManager gameManager, int playerId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Player player = gameManager.Players.Find(p => p.Id == playerId);

    public string PlayerName => player.Name;
    public int PreviousMoney => player.PreviousMoney;
    public int BetAmount => player.BetAmount;
    public Slug SelectedSlug => player.SelectedSlug;
    public int Gain => player.Gain;
    public int PlayerCurrentMoney => player.CurrentMoney;
    public double PreviousOdds => player.SelectedSlug.PreviousOdds;
}
