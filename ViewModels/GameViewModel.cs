using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Plugin.Maui.Audio;
using Slugrace.Messages;
using Slugrace.Models;
using Slugrace.Views;
using System.Collections.ObjectModel;

namespace Slugrace.ViewModels;

[QueryProperty(nameof(Game), "Game")]
public partial class GameViewModel : ObservableObject
{
    SoundViewModel soundViewModel;
    private readonly IPopupService popupService;

    public AccidentViewModel AccidentViewModel;

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
    private uint secondTime;
      
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

    [ObservableProperty]
    private bool muted;

    [ObservableProperty]
    private bool accidentShouldHappen;

    [ObservableProperty]
    private IAudioPlayer accidentSoundPlayer;

    [ObservableProperty]
    private uint afterAccidentTime = 0;

    // Game screenshots used for the InstructionsPage
    [ObservableProperty]
    private List<string> screenshots;    

    public GameViewModel(SoundViewModel soundViewModel, IPopupService popupService)
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

        this.soundViewModel = soundViewModel;
        this.popupService = popupService;

        WeakReferenceMessenger.Default.Register<PlayerBetAmountChangedMessage>(this, (r, m) =>
            OnBetAmountChangedMessageReceived(m.Value));

        WeakReferenceMessenger.Default.Register<PlayerSelectedSlugChangedMessage>(this, (r, m) =>
            OnSelectedSlugChangedMessageReceived(m.Value));

#if WINDOWS
        Screenshots = 
        [
            "settings_windows.png",
            "race_bets_windows.png",
            "race_results_windows.png",
            "gameover_windows.png"
        ];
#endif

#if ANDROID
        Screenshots =
        [
            "settings_android.png",
            "race_bets_android.png",
            "race_results_android.png",
            "gameover_android.png"
        ];
#endif
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
                DefaultEyeImageUrl = value.Slugs[0].DefaultEyeImageUrl,
                DefaultBodyImageUrl = value.Slugs[0].DefaultBodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[0].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[0].BaseOdds,
                WinSound = value.Slugs[0].WinSound
            },
            new()
            {
                Name = value.Slugs[1].Name,
                ImageUrl = value.Slugs[1].ImageUrl,
                EyeImageUrl = value.Slugs[1].EyeImageUrl,
                BodyImageUrl = value.Slugs[1].BodyImageUrl,
                DefaultEyeImageUrl = value.Slugs[1].DefaultEyeImageUrl,
                DefaultBodyImageUrl = value.Slugs[1].DefaultBodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[1].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[1].BaseOdds,
                WinSound = value.Slugs[1].WinSound
            },
            new()
            {
                Name = value.Slugs[2].Name,
                ImageUrl = value.Slugs[2].ImageUrl,
                EyeImageUrl = value.Slugs[2].EyeImageUrl,
                BodyImageUrl = value.Slugs[2].BodyImageUrl,
                DefaultEyeImageUrl = value.Slugs[2].DefaultEyeImageUrl,
                DefaultBodyImageUrl = value.Slugs[2].DefaultBodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[2].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[2].BaseOdds,
                WinSound = value.Slugs[2].WinSound
            },
            new()
            {
                Name = value.Slugs[3].Name,
                ImageUrl = value.Slugs[3].ImageUrl,
                EyeImageUrl = value.Slugs[3].EyeImageUrl,
                BodyImageUrl = value.Slugs[3].BodyImageUrl,
                DefaultEyeImageUrl = value.Slugs[3].DefaultEyeImageUrl,
                DefaultBodyImageUrl = value.Slugs[3].DefaultBodyImageUrl,
                WinNumber = 0,
                Odds = Math.Round(value.Slugs[3].BaseOdds + new Random().Next(0, 10) / 100, 2),
                PreviousOdds = value.Slugs[3].BaseOdds,
                WinSound = value.Slugs[3].WinSound
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

        _ = soundViewModel.PlayBackgroundMusic("Game", "Background Music.mp3");
    }

    [RelayCommand]
    async Task StartRace()
    {
        Slugs[0].RunningTime = (uint)new Random().Next(6000, 14000);
        Slugs[1].RunningTime = (uint)new Random().Next(8000, 14000);
        Slugs[2].RunningTime = (uint)new Random().Next(8000, 14000);
        Slugs[3].RunningTime = (uint)new Random().Next(10000, 16000);
                
        // Check for accident.   
        bool thereIsAnAccident;
        bool accidentExpected = new Random().Next(0, 4) == 0;

        // Should there be an accident?
        if (RaceNumber > 5 && accidentExpected)
        {           
            // If so, then...
            thereIsAnAccident = true;

            // Which one?
            AccidentType[] accidentTypes = (AccidentType[])Enum.GetValues(typeof(AccidentType));
            var type = accidentTypes[new Random().Next(0, accidentTypes.Length)];
            AccidentViewModel = new AccidentViewModel(type);

            // Which slug should be affected?
            AccidentViewModel.AffectedSlug = Slugs[new Random().Next(0, Slugs.Count)];
                        
            // When should it happen?
            AccidentViewModel.TimePosition = (uint)new Random().Next((int)(AccidentViewModel.AffectedSlug.RunningTime * .2),
                (int)(AccidentViewModel.AffectedSlug.RunningTime * .4));

            // Modify affected slug's running time
            if (AccidentViewModel.Duration > 0)
            {
                if (AccidentViewModel.AccidentType == AccidentType.Grass)
                {
                    AfterAccidentTime = AccidentViewModel.AffectedSlug.RunningTime / 4;

                    AccidentViewModel.AffectedSlug.RunningTime = AccidentViewModel.TimePosition
                    + AccidentViewModel.Duration + AfterAccidentTime;
                }

                if (AccidentViewModel.AccidentType == AccidentType.Electroshock)
                {
                    AfterAccidentTime = AccidentViewModel.AffectedSlug.RunningTime / 4;

                    AccidentViewModel.AffectedSlug.RunningTime = AccidentViewModel.TimePosition
                    + AccidentViewModel.Duration + AfterAccidentTime;
                }
            }
        }
        else
        {
            thereIsAnAccident = false;
        }

        // Set race-related times
        uint[] runningTimes = [
            Slugs[0].RunningTime,
            Slugs[1].RunningTime,
            Slugs[2].RunningTime,
            Slugs[3].RunningTime
        ];
               
        RaceTime = runningTimes.Max();
        MinTime = runningTimes.Min();
        FinishTime = (uint)(.79 * MinTime);
        SecondTime = runningTimes.Order().ToArray()[1];
                
        AccidentShouldHappen = thereIsAnAccident;
                
        // Start race
        RaceStatus = RaceStatus.Started;

        if (RaceNumber == 1 && GameEndingCondition == EndingCondition.Time)
        {
            gameTimer.Start();
        }

        _ = soundViewModel.PlaySound("Game", "Go.mp3", .2);

        await RunRace();        
    }

    private async Task RunRace()
    {
        _ = soundViewModel.PlaySound("Game", "Slugs Running.mp3", .5, true);

        // Modify finish time if the fastest slug has an accident.       
        if (AccidentViewModel?.AffectedSlug.RunningTime == MinTime)
        {
            if (AccidentViewModel.Duration == 0)
            {
                FinishTime = (uint)(.79 * SecondTime);
            }
            else
            {
                uint secondFinishTime = (uint)(SecondTime * .79);

                if (secondFinishTime < FinishTime)
                {
                    FinishTime = secondFinishTime;
                    MinTime = SecondTime;
                }
            }
        }
               
        await Task.Delay((int)FinishTime);

        RaceWinnerSlug = Slugs.Where(s => s.RunningTime == MinTime).FirstOrDefault();

        if (AccidentShouldHappen 
            && RaceWinnerSlug == AccidentViewModel.AffectedSlug 
            && AccidentViewModel.Duration == 0)
        {
            RaceWinnerSlug = Slugs.Where(s => s.RunningTime == SecondTime).FirstOrDefault();
        }
        
        _ = soundViewModel.PlaySound("Slugs Winning", RaceWinnerSlug.WinSound);

        await Task.Delay((int)(RaceTime - FinishTime));

        RaceWinnerSlug.IsRaceWinner = true;

        soundViewModel.Clean();

        HandleSlugsAfterRace();

        HandlePlayersAfterRace();

        await FinishRace();
    }

    public void PlayAccidentSound(bool loop = false, bool loopingAccidentSound = false)
    {
        _ = soundViewModel.PlaySound("Accidents", AccidentViewModel.Sound, loop: loop, 
            loopingAccidentSound: loopingAccidentSound);
    }

    public void StopAccidentSound()
    {
        soundViewModel.Clean(true);
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

        await soundViewModel.Attenuate();

        gameOverPageDelayTimer.Tick += async (sender, e) =>
        {
            gameOverPageDelayTimer.Stop();

            IsShowingFinalResults = false;

            _ = soundViewModel.PlaySound("Game", "Game Over.mp3", .5);

            // Navigate to GameOverPage
            await Shell.Current.GoToAsync($"{nameof(GameOverPage)}",
                new Dictionary<string, object>
                {
                    {"Game", this }
                });
        };  
                
        soundViewModel.Stop();
    }

    [RelayCommand]
    void NextRace()
    {
        soundViewModel.Clean();
        soundViewModel.Clean(true);

        RaceStatus = RaceStatus.NotYetStarted;
        RaceNumber++;

        RaceWinnerSlug = null;

        AccidentViewModel = null;

        AccidentShouldHappen = false;

        foreach (var player in Players)
        {
            player.BetAmount = 0;
            player.SelectedSlug = null;
        }

        foreach (var slug in Slugs)
        {
            if (slug.BodyImageUrl != slug.DefaultBodyImageUrl)
            {
                slug.BodyImageUrl = slug.DefaultBodyImageUrl;
            }

            if (slug.EyeImageUrl != slug.DefaultEyeImageUrl)
            {
                slug.EyeImageUrl = slug.DefaultEyeImageUrl;
            }
        }
    }

    public void DisplayAccidentPopup()
    {
        popupService.ShowPopup<AccidentPopupViewModel>(
            onPresenting: viewModel => viewModel.ShowAccidentInfo(AccidentViewModel));
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

    [RelayCommand]
    public void MuteUnmute()
    {
        soundViewModel.MuteUnmute();
        Muted = soundViewModel.Muted;
    }
}
