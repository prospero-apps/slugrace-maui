using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class PlayerBetViewModel(GameManager gameManager, int playerId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Player player = gameManager.Players.Find(p => p.Id == playerId);

    public string PlayerName => player.Name;

    public int BetAmount
    {
        get => player.BetAmount;
        set
        {
            if (player.BetAmount != value)
            {
                player.BetAmount = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(BetAmountIsValid));

                //WeakReferenceMessenger.Default.Send(new BetAmountChangedMessage(value));
            }
        }
    }

    public Slug SelectedSlug
    {
        get => player.SelectedSlug;
        set
        {
            if(player.SelectedSlug != value)
            {
                player.SelectedSlug = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(SelectedSlugValid));

                //WeakReferenceMessenger.Default.Send(new SelectedSlugChangedMessage(value));
            }
        }
    }
}
