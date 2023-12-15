using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class SlugViewModel : ObservableObject
{
    private Slug slug;

    public string Name
    {
        get => slug.Name;
        set
        {
            if (slug.Name != value)
            {
                slug.Name = value;
                OnPropertyChanged();
            }
        }
    }

    public string ImageUrl
    {
        get => slug.ImageUrl;
        set
        {
            if (slug.ImageUrl != value)
            {
                slug.ImageUrl = value;
                OnPropertyChanged();
            }
        }
    }

    public string EyeImageUrl
    {
        get => slug.EyeImageUrl;
        set
        {
            if (slug.EyeImageUrl != value)
            {
                slug.EyeImageUrl = value;
                OnPropertyChanged();
            }
        }
    }

    public string BodyImageUrl
    {
        get => slug.BodyImageUrl;
        set
        {
            if (slug.BodyImageUrl != value)
            {
                slug.BodyImageUrl = value;
                OnPropertyChanged();
            }
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WinPercentage))]
    private int currentRaceNumber;

    public int WinNumber
    {
        get => slug.WinNumber;
        set
        {
            if (slug.WinNumber != value)
            {
                slug.WinNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(WinPercentage));
                OnPropertyChanged(nameof(WinNumberText));
            }
        }
    }

    public string WinNumberText => WinNumber == 1 ? $"{WinNumber} win" : $"{WinNumber} wins";

    [ObservableProperty]
    private int winPercentage;

    public bool IsRaceWinner
    {
        get => slug.IsRaceWinner;
        set
        {
            if (slug.IsRaceWinner != value)
            {
                slug.IsRaceWinner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Odds));
            }
        }
    }

    public double Odds
    {
        get => slug.Odds;
        set
        {
            if (slug.Odds != value)
            {
                slug.Odds = value;
                OnPropertyChanged();
            }
        }
    }

    public double PreviousOdds
    {
        get => slug.PreviousOdds;
        set
        {
            if (slug.PreviousOdds != value)
            {
                slug.PreviousOdds = value;
                OnPropertyChanged();
            }
        }
    }

    public string WinSound
    {
        get => slug.WinSound;
        set
        {
            if (slug.WinSound != value)
            {
                slug.WinSound = value;
                OnPropertyChanged();
            }
        }
    }

    [ObservableProperty]
    private uint runningTime;

    public SlugViewModel()
    {
        slug = new Slug();

        CurrentRaceNumber = 1;

        WeakReferenceMessenger.Default.Register<RaceFinishedMessage>(this, (r, m) =>
            OnRaceFinishedMessageReceived(m.Value));
    }
        

    private void OnRaceFinishedMessageReceived(RaceStatus value)
    {
        WinPercentage = (int)((double)WinNumber / CurrentRaceNumber * 100);

        PreviousOdds = Odds;

        Odds = IsRaceWinner
        ? Math.Round(Math.Max(1.01, Math.Min(Odds * .96, 20)), 2)
        : Math.Round(Math.Max(1.01, Math.Min(Odds * 1.03, 20)), 2);
    }
        
    public void RecalculateStats(int raceNumber)
    {
        CurrentRaceNumber = raceNumber;

        if (IsRaceWinner)
        {
            WinNumber++;
        }
    }
}
