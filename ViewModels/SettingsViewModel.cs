using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using System.Collections.ObjectModel;

namespace Slugrace.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    const int minRaces = 1;
    const int maxRaces = 100;
    const int minTime = 1;
    const int maxTime = 120;

    private GameManager gameManager;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllSettingsAreValid))]
    private ObservableCollection<PlayerSettingsViewModel> players;    

    public EndingCondition GameEndingCondition
    {
        get => gameManager.EndingCondition;
        set
        {
            if (gameManager.EndingCondition != value)
            {
                gameManager.EndingCondition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllSettingsAreValid));
            }
        }
    }

    public int NumberOfRacesSet
    {
        get => gameManager.NumberOfRacesSet;
        set
        {
            if (gameManager.NumberOfRacesSet != value)
            {
                gameManager.NumberOfRacesSet = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(AllSettingsAreValid));
            }
        }
    }

    public int GameTimeSet
    {
        get => gameManager.GameTimeSet;
        set
        {
            if (gameManager.GameTimeSet != value)
            {
                gameManager.GameTimeSet = value;
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

    public SettingsViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;

        Players = new ObservableCollection<PlayerSettingsViewModel>
        {
            new() { PlayerId = 1, PlayerIsInGame = true },
            new() { PlayerId = 2, PlayerIsInGame = true },
            new() { PlayerId = 3, PlayerIsInGame = false },
            new() { PlayerId = 4, PlayerIsInGame = false }
        };

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
}
