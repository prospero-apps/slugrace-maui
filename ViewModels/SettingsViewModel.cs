using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using Slugrace.Models;
using Slugrace.Views;
using System.Collections.ObjectModel;

namespace Slugrace.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    const int minRaces = 1;
    const int maxRaces = 100;
    const int minTime = 1;
    const int maxTime = 120;

    private Game game;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllSettingsAreValid))]
    private ObservableCollection<PlayerSettingsViewModel> players;

    public EndingCondition GameEndingCondition
    {
        get => game.GameEndingCondition;
        set
        {
            if (game.GameEndingCondition != value)
            {
                game.GameEndingCondition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllSettingsAreValid));
            }
        }
    }

    public int NumberOfRacesSet
    {
        get => game.NumberOfRacesSet;
        set
        {
            if (game.NumberOfRacesSet != value)
            {
                game.NumberOfRacesSet = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllSettingsAreValid));
            }
        }
    }

    public int GameTimeSet
    {
        get => game.GameTimeSet;
        set
        {
            if (game.GameTimeSet != value)
            {
                game.GameTimeSet = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllSettingsAreValid));
            }
        }
    }

    public bool MaxRacesIsValid => Helpers.ValueIsInRange(NumberOfRacesSet,
        minRaces, maxRaces);

    public bool MaxTimeIsValid => Helpers.ValueIsInRange(GameTimeSet,
        minTime, maxTime);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllSettingsAreValid))]
    [NotifyPropertyChangedFor(nameof(OnlyOnePlayer))]
    [NotifyPropertyChangedFor(nameof(RacesEndingConditionSet))]
    private int currentNumberOfPlayers = 2;

    public bool OnlyOnePlayer => CurrentNumberOfPlayers == 1;

    public bool RacesEndingConditionSet
        => (CurrentNumberOfPlayers == 1 && GameEndingCondition != EndingCondition.Time)
        || GameEndingCondition == EndingCondition.Races;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllSettingsAreValid))]
    private string changedPlayerName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllSettingsAreValid))]
    private int? changedPlayerInitialMoney;

    public bool AllSettingsAreValid
    {
        get
        {
            bool conditionPlayers = Players.All(p => p.PlayerIsValid);
            bool conditionMoney = GameEndingCondition == EndingCondition.Money;
            bool conditionRaces = GameEndingCondition == EndingCondition.Races && MaxRacesIsValid;
            bool conditionTime = GameEndingCondition == EndingCondition.Time && MaxTimeIsValid;

            return conditionPlayers && (conditionMoney || conditionRaces || conditionTime);
        }
    }

    public SettingsViewModel()
    {
        game = new Game();

        Players =
        [
            new() { PlayerId = 1, PlayerIsInGame = true },
            new() { PlayerId = 2, PlayerIsInGame = true },
            new() { PlayerId = 3, PlayerIsInGame = false },
            new() { PlayerId = 4, PlayerIsInGame = false }
        ];
        
        WeakReferenceMessenger.Default.Register<PlayerNameChangedMessage>(this, (r, m) =>
            OnPlayerNameChangedMessageReceived(m.Value));

        WeakReferenceMessenger.Default.Register<PlayerInitialMoneyChangedMessage>(this, (r, m) =>
            OnPlayerInitialMoneyChangedMessageReceived(m.Value));
    }

    private void OnPlayerNameChangedMessageReceived(string value)
    {
        ChangedPlayerName = value;
    }

    private void OnPlayerInitialMoneyChangedMessageReceived(int? value)
    {
        ChangedPlayerInitialMoney = value;
    }

    [RelayCommand]
    void CreatePlayerList(int numberOfPlayers)
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].PlayerIsInGame = i < numberOfPlayers;
        }

        CurrentNumberOfPlayers = numberOfPlayers;

        if (OnlyOnePlayer)
        {
            GameEndingCondition = EndingCondition.Races;
        }
    }

    [RelayCommand]
    void SetEndingCondition(string condition)
    {
        GameEndingCondition = condition switch
        {
            "money" => EndingCondition.Money,
            "races" => EndingCondition.Races,
            "time" => EndingCondition.Time,
            _ => EndingCondition.Money
        };
    }
       
    [RelayCommand]
    async Task StartGame()
    {
        // Populate the Game object        
        game.Slugs =
        [
            new Slug 
            { 
                Name = "Speedster", 
                BaseOdds = 1.33, 
                ImageUrl = "speedster.png",
                EyeImageUrl = "speedster_eye.png",
                BodyImageUrl = "speedster_body.png",
                WinSound = "Speedster Win.mp3"
            },
            new Slug 
            { 
                Name = "Trusty", 
                BaseOdds = 1.59, 
                ImageUrl = "trusty.png",
                EyeImageUrl = "trusty_eye.png",
                BodyImageUrl = "trusty_body.png",
                WinSound = "Trusty Win.mp3"
            },
            new Slug 
            { 
                Name = "Iffy", 
                BaseOdds = 2.5, 
                ImageUrl = "iffy.png",
                EyeImageUrl = "iffy_eye.png",
                BodyImageUrl = "iffy_body.png",
                WinSound = "Iffy Win.mp3"
            },
            new Slug 
            { 
                Name = "Slowpoke", 
                BaseOdds = 2.89, 
                ImageUrl = "slowpoke.png",
                EyeImageUrl = "slowpoke_eye.png",
                BodyImageUrl = "slowpoke_body.png",
                WinSound = "Slowpoke Win.mp3"
            }
        ];
               
        var playersInGame = Players.Where(p => p.PlayerIsInGame).ToList();

        game.Players = [];
               
        foreach (var player in playersInGame)
        {
            game.Players.Add(new Player
            {
                Id = player.PlayerId,
                Name = string.IsNullOrEmpty(player.PlayerName) ? "Player " + player.PlayerId : player.PlayerName,
                IsInGame = true,
                InitialMoney = player.PlayerInitialMoney,
                CurrentMoney = player.PlayerInitialMoney
            });
        }

        // Navigate to RacePage
        await Shell.Current.GoToAsync($"{nameof(RacePage)}",
            new Dictionary<string, object>
            {
                {"Game", game }
            });
    }
}

