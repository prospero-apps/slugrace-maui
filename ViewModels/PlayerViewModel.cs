using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class PlayerViewModel : ObservableObject
{
    private Player player;

    public string Name
    {
        get => player.Name;
        set
        {
            if (player.Name != value)
            {
                player.Name = value;
                OnPropertyChanged();
            }
        }
    }

    public int InitialMoney
    {
        get => player.InitialMoney;
        set
        {
            if (player.InitialMoney != value)
            {
                player.InitialMoney = value;
                OnPropertyChanged();
            }
        }
    }

    public int CurrentMoney
    {
        get => player.CurrentMoney;
        set
        {
            if (player.CurrentMoney != value)
            {
                player.CurrentMoney = value;
                OnPropertyChanged();
            }
        }
    }

    public int BetAmount
    {
        get => player.BetAmount;
        set
        {
            if (player.BetAmount != value)
            {
                player.BetAmount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(BetAmountIsValid));
                OnPropertyChanged(nameof(PlayerIsValid));

                WeakReferenceMessenger.Default.Send(new PlayerBetAmountChangedMessage(value));
            }
        }
    }
        
    public int PreviousMoney
    {
        get => player.PreviousMoney;
        set
        {
            if (player.PreviousMoney != value)
            {
                player.PreviousMoney = value;
                OnPropertyChanged();
            }
        }
    }

    public int WonOrLostMoney
    {
        get => player.WonOrLostMoney;
        set
        {
            if (player.WonOrLostMoney != value)
            {
                player.WonOrLostMoney = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsInGame
    {
        get => player.IsInGame;
        set
        {
            if (player.IsInGame != value)
            {
                player.IsInGame = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsBankrupt
    {
        get => player.IsBankrupt;
        set
        {
            if (player.IsBankrupt != value)
            {
                player.IsBankrupt = value;
                OnPropertyChanged();
            }
        }
    }

    [ObservableProperty]
    private List<SlugViewModel> slugs;
      
    private SlugViewModel selectedSlug;

    public SlugViewModel SelectedSlug
    {
        get => selectedSlug;
        set
        {
            if (selectedSlug != value)
            {
                selectedSlug = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedSlugIsValid));
                OnPropertyChanged(nameof(PlayerIsValid));

                WeakReferenceMessenger.Default.Send(new PlayerSelectedSlugChangedMessage(value));
            }
        }
    }
       
    public bool BetAmountIsValid => Helpers.ValueIsInRange(BetAmount,
        1, CurrentMoney);

    public bool SelectedSlugIsValid => SelectedSlug != null;

    public bool PlayerIsValid => IsBankrupt || (BetAmountIsValid && SelectedSlugIsValid);

    [ObservableProperty]
    private string resultMessage;

    public PlayerViewModel()
    {
        player = new Player();
    }

    [RelayCommand]
    void SelectSlug(string name)
    {
        var slug = Slugs.Find(s => s.Name == name);
        SelectedSlug = slug;
    }

    public void CalculateMoney(SlugViewModel raceWinnerSlug)
    {
        PreviousMoney = CurrentMoney;
        
        bool wonRace = SelectedSlug == raceWinnerSlug;

        WonOrLostMoney = (int)(wonRace
            ? BetAmount * (SelectedSlug.Odds - 1)
            : -BetAmount);

        CurrentMoney += WonOrLostMoney;

        ResultMessage = wonRace
            ? (WonOrLostMoney == 0 ? $"- won less than $1" : $"- won ${WonOrLostMoney}")
            : $"- lost ${Math.Abs(WonOrLostMoney)}";
    }
}
