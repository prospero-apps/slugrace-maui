using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using Slugrace.Models;
using Slugrace.Views;
using System.Collections.ObjectModel;

namespace Slugrace.ViewModels;

[QueryProperty(nameof(Game), "Game")]
public partial class GameViewModel : ObservableObject
{
    [ObservableProperty]
    private Game game;

    readonly IDispatcherTimer gameTimer;
    readonly IDispatcherTimer gameOverPageDelayTimer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(EndingConditionIsRaces))]
    [NotifyPropertyChangedFor(nameof(EndingConditionIsTime))]
    private EndingCondition gameEndingCondition;

    [ObservableProperty]
    private List<SlugViewModel> slugs;

    [ObservableProperty]
    private List<PlayerViewModel> players;

    [ObservableProperty]
    private ObservableCollection<PlayerViewModel> playersStillInGame;

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

                if (raceStatus == RaceStatus.Finished)
                {
                    WeakReferenceMessenger.Default.Send(new RaceFinishedMessage(value));
                }
            }
        }
    }
    
    [ObservableProperty]
    private string gameOverReason;

    public bool EndingConditionIsRaces => GameEndingCondition == EndingCondition.Races;

    public bool EndingConditionIsTime => GameEndingCondition == EndingCondition.Time;      
        
    private int raceNumber;
    public int RaceNumber
    {
        get => raceNumber;
        set
        {
            if (raceNumber != value)
            {
                raceNumber = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RacesFinished));
                OnPropertyChanged(nameof(RacesToGo));
            }
        }
    }

    [ObservableProperty]
    private uint raceTime;

    [ObservableProperty]
    private uint minTime;

    [ObservableProperty]
    private uint finishTime;

    [ObservableProperty]
    private bool isShowingFinalResults;

    [ObservableProperty]
    private int numberOfRacesSet;
    
    public int RacesFinished => RaceNumber - 1;

    public int RacesToGo => NumberOfRacesSet - RacesFinished;
          
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeRemaining))]
    private TimeSpan gameTimeSet;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TimeRemaining))]
    private TimeSpan timeElapsed;

    public TimeSpan TimeRemaining => GameTimeSet - TimeElapsed;

    [ObservableProperty]
    private List<PlayerViewModel> winners;

    [ObservableProperty]
    private SlugViewModel raceWinnerSlug;
        
    private int changedBetAmount;
    public int ChangedBetAmount
    {
        get => changedBetAmount;
        set
        {
            OnPropertyChanged();
            OnPropertyChanged(nameof(AllPlayersAreValid));

            if (changedBetAmount != value)
            {
                changedBetAmount = value;
            }
        }
    }
        
    private SlugViewModel changedSelectedSlug;
    public SlugViewModel ChangedSelectedSlug
    {
        get => changedSelectedSlug;
        set
        {
            OnPropertyChanged();
            OnPropertyChanged(nameof(AllPlayersAreValid));

            if (changedSelectedSlug != value)
            {
                changedSelectedSlug = value;
            }
        }
    }

    public bool? AllPlayersAreValid => Players?.All(p => p.PlayerIsValid);
       
    public GameViewModel()
    {
        gameTimer = Application.Current.Dispatcher.CreateTimer();
        gameTimer.Interval = TimeSpan.FromSeconds(1);
        gameTimer.Tick += async (sender, e) =>
        {
            if (TimeRemaining > TimeSpan.Zero)
            {
                TimeElapsed += TimeSpan.FromSeconds(1);
            }

            if (TimeRemaining == TimeSpan.Zero && RaceStatus == RaceStatus.Finished)
            {
                await CheckForGameOver();
            }
        };

        gameOverPageDelayTimer = Application.Current.Dispatcher.CreateTimer();
        gameOverPageDelayTimer.Interval = TimeSpan.FromSeconds(3);

        WeakReferenceMessenger.Default.Register<PlayerBetAmountChangedMessage>(this, (r, m) =>
            OnBetAmountChangedMessageReceived(m.Value));

        WeakReferenceMessenger.Default.Register<PlayerSelectedSlugChangedMessage>(this, (r, m) =>
            OnSelectedSlugChangedMessageReceived(m.Value));
    }
       
    private void OnBetAmountChangedMessageReceived(int value)
    {
        ChangedBetAmount = value;
    }

    private void OnSelectedSlugChangedMessageReceived(SlugViewModel value)
    {
        ChangedSelectedSlug = value;
    }

    partial void OnGameChanged(Game value)
    {
        GameEndingCondition = value.GameEndingCondition;

        RaceStatus = RaceStatus.NotYetStarted;

        RaceNumber = 1;

        NumberOfRacesSet = value.NumberOfRacesSet;

        GameTimeSet = TimeSpan.FromMinutes(value.GameTimeSet);

        Winners = [];

        Slugs =
        [
            new()
            {
                Name = value.Slugs[0].Name,
                ImageUrl = value.Slugs[0].ImageUrl,
                EyeImageUrl = value.Slugs[0].EyeImageUrl,
                BodyImageUrl = value.Slugs[0].BodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[0].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[0].BaseOdds
            },
            new()
            {
                Name = value.Slugs[1].Name,
                ImageUrl = value.Slugs[1].ImageUrl,
                EyeImageUrl = value.Slugs[1].EyeImageUrl,
                BodyImageUrl = value.Slugs[1].BodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[1].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[1].BaseOdds
            },
            new()
            {
                Name = value.Slugs[2].Name,
                ImageUrl = value.Slugs[2].ImageUrl,
                EyeImageUrl = value.Slugs[2].EyeImageUrl,
                BodyImageUrl = value.Slugs[2].BodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[2].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[2].BaseOdds
            },
            new()
            {
                Name = value.Slugs[3].Name,
                ImageUrl = value.Slugs[3].ImageUrl,
                EyeImageUrl = value.Slugs[3].EyeImageUrl,
                BodyImageUrl = value.Slugs[3].BodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[3].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[3].BaseOdds
            },
        ];
                
        List<PlayerViewModel> players = [];

        foreach (var player in value.Players)
        {
            players.Add
            (
                new()
                {
                    Name = player.Name,
                    InitialMoney = player.InitialMoney,
                    CurrentMoney = player.CurrentMoney,
                    BetAmount = 0,
                    IsInGame = player.IsInGame,
                    PreviousMoney = player.CurrentMoney,
                    WonOrLostMoney = 0,
                    Slugs = Slugs,
                    SelectedSlug = null
                }
            );
        }

        Players = players;

        PlayersStillInGame = Players.ToObservableCollection();
    }

    [RelayCommand]
    async Task StartRace()
    {
        // Start race
        RaceStatus = RaceStatus.Started;

        if (RaceNumber == 1 && GameEndingCondition == EndingCondition.Time)
        {
            gameTimer.Start();
        }

        await RunRace();      
    }

    private async Task RunRace()
    {
        await Task.Delay((int)FinishTime);

        RaceWinnerSlug = Slugs.Where(s => s.RunningTime == MinTime).FirstOrDefault();

        await Task.Delay((int)(RaceTime - FinishTime));
                
        RaceWinnerSlug.IsRaceWinner = true;

        HandleSlugsAfterRace();

        HandlePlayersAfterRace();

        await FinishRace();
    }
       
    private void HandleSlugsAfterRace()
    {
        foreach (var slug in Slugs)
        {
            if (slug != RaceWinnerSlug)
            {
                slug.IsRaceWinner = false;
            }

            slug.RecalculateStats(RaceNumber);
        }
    }

    private void HandlePlayersAfterRace()
    {
        foreach (var player in Players)
        {
            player.CalculateMoney(RaceWinnerSlug);

            if (player.CurrentMoney == 0)
            {
                player.IsBankrupt = true;
            }

            PlayersStillInGame = PlayersStillInGame.Where(p => !p.IsBankrupt).ToObservableCollection();
        }
    }

    [RelayCommand]
    async Task EndGameManually()
    {
        await CheckForGameOver(true);
    }

    private async Task FinishRace()
    {
        RaceStatus = RaceStatus.Finished;

        await CheckForGameOver();
    }

    private async Task CheckForGameOver(bool endedManually = false)
    {
        // This is for the case when the game is ended manually
        if (endedManually)
        {
            GetWinners();
            GameOverReason = "You ended the game manually.";
            await EndGame();
        }
        // This works the same for each ending condition.
        // scenario 1: there's only 1 player with money (except one-player mode) - it's the winner
        else if (Players.Count > 1 && PlayersStillInGame.Count == 1)
        {
            Winners.Add(PlayersStillInGame[0]);
            GameOverReason = "There's only one player with any money left.";
            await EndGame();
        }
        // scenario 2: all players go bankrupt simultaneously - no winner
        else if (PlayersStillInGame.Count == 0)
        {
            GameOverReason = Players.Count == 1 ? "You are bankrupt." : "All players are bankrupt.";
            await EndGame();
        }
        // This works for the Races ending condition
        else if (GameEndingCondition == EndingCondition.Races && RaceNumber == NumberOfRacesSet)
        {
            GetWinners();            

            GameOverReason = "The number of races you set has been reached.";
            await EndGame();
        }
        // This works for the Time ending condition
        else if (GameEndingCondition == EndingCondition.Time && TimeRemaining == TimeSpan.Zero)
        {
            GetWinners();

            GameOverReason = "The game time you set is up.";
            await EndGame();
        }
    }

    private void GetWinners()
    {
        int maxMoney = PlayersStillInGame.Max(p => p.CurrentMoney);

        foreach (var player in PlayersStillInGame)
        {
            if (player.CurrentMoney == maxMoney)
            {
                Winners.Add(player);
            }
        }
    }

    async Task EndGame()
    {
        gameTimer.Stop();

        IsShowingFinalResults = true;

        gameOverPageDelayTimer.Start();

        gameOverPageDelayTimer.Tick += async (sender, e) =>
        {
            gameOverPageDelayTimer.Stop();

            IsShowingFinalResults = false;

            // Navigate to GameOverPage
            await Shell.Current.GoToAsync($"{nameof(GameOverPage)}",
                new Dictionary<string, object>
                {
                    {"Game", this }
                });
        };        
    }

    [RelayCommand]
    void NextRace()
    {
        RaceStatus = RaceStatus.NotYetStarted;
        RaceNumber++;

        RaceWinnerSlug = null;

        foreach (var player in Players)
        {
            player.BetAmount = 0;
            player.SelectedSlug = null;
        }
    }

    [RelayCommand]
    async Task SeeInstructions()
    {       
        // Navigate to InstructionsPage
        await Shell.Current.GoToAsync($"{nameof(InstructionsPage)}");
    }

    [RelayCommand]
    async Task NavigateBack()
    {       
        await Shell.Current.GoToAsync("..");
    }
}


