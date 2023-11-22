using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Slugrace.Messages;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class PlayerSettingsViewModel : ObservableObject
{
    const int maxNameLength = 10;
    const int minInitialMoney = 10;
    const int maxInitialMoney = 5000;

    private Player player;

    public int PlayerId
    {
        get => player.Id;
        set
        {
            if (player.Id != value)
            {
                player.Id = value;
                OnPropertyChanged();
            }
        }
    }

    public string PlayerName
    {
        get => player.Name;
        set
        {
            if (player.Name != value)
            {
                player.Name = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NameIsValid));
                OnPropertyChanged(nameof(PlayerIsValid));

                WeakReferenceMessenger.Default.Send(new PlayerNameChangedMessage(value));
            }
        }
    }

    public int PlayerInitialMoney
    {
        get => player.InitialMoney;
        set
        {
            if (player.InitialMoney != value)
            {
                player.InitialMoney = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(InitialMoneyIsValid));
                OnPropertyChanged(nameof(PlayerIsValid));

                WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(value));
            }
        }
    }

    public bool PlayerIsInGame
    {
        get => player.IsInGame;
        set
        {
            if (player.IsInGame != value)
            {
                player.IsInGame = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PlayerIsValid));
            }
        }
    }

    public bool NameIsValid => (PlayerName == null) || (PlayerName?.Length <= maxNameLength);

    public bool InitialMoneyIsValid => Helpers.ValueIsInRange(PlayerInitialMoney,
        minInitialMoney, maxInitialMoney);

    public bool PlayerIsValid
    {
        get
        {
            if (PlayerIsInGame)
            {
                return NameIsValid && InitialMoneyIsValid;
            }
            else
            {
                return true;
            }
        }
    }

    public PlayerSettingsViewModel()
    {
        player = new Player();
    }
}

// ********************************************************************

//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Messaging;
//using Slugrace.Messages;
//using Slugrace.Models;

//namespace Slugrace.ViewModels;

//public partial class PlayerSettingsViewModel : ObservableObject
//{
//    const int maxNameLength = 10;
//    const int minInitialMoney = 10;
//    const int maxInitialMoney = 5000;

//    private Player player;

//    public int PlayerId
//    {
//        get => player.Id;
//        set
//        {
//            if (player.Id != value)
//            {
//                player.Id = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string PlayerName
//    {
//        get => player.Name;
//        set
//        {
//            if (player.Name != value)
//            {
//                player.Name = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(NameIsValid));
//                OnPropertyChanged(nameof(PlayerIsValid));

//                WeakReferenceMessenger.Default.Send(new PlayerNameChangedMessage(value));
//            }
//        }
//    }

//    public int PlayerInitialMoney
//    {
//        get => player.InitialMoney;
//        set
//        {
//            if (player.InitialMoney != value)
//            {
//                player.InitialMoney = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(InitialMoneyIsValid));
//                OnPropertyChanged(nameof(PlayerIsValid));

//                WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(value));
//            }
//        }
//    }

//    public bool PlayerIsInGame
//    {
//        get => player.IsInGame;
//        set
//        {
//            if (player.IsInGame != value)
//            {
//                player.IsInGame = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(PlayerIsValid));
//            }
//        }
//    }

//    public bool NameIsValid => (PlayerName == null) || (PlayerName?.Length <= maxNameLength);

//    public bool InitialMoneyIsValid => Helpers.ValueIsInRange(PlayerInitialMoney,
//        minInitialMoney, maxInitialMoney);

//    public bool PlayerIsValid
//    {
//        get
//        {
//            if (PlayerIsInGame)
//            {
//                return NameIsValid && InitialMoneyIsValid;
//            }
//            else
//            {
//                return true;
//            }
//        }
//    }

//    public PlayerSettingsViewModel()
//    {
//        player = new Player();
//    }
//}

// ************************************************************************

//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Messaging;
//using Slugrace.Messages;
//using Slugrace.Models;

//namespace Slugrace.ViewModels;

//public partial class PlayerSettingsViewModel : ObservableObject
//{
//    const int maxNameLength = 10;
//    const int minInitialMoney = 10;
//    const int maxInitialMoney = 5000;

//    private Player player;

//    public int PlayerId
//    {
//        get => player.Id;
//        set
//        {
//            if (player.Id != value)
//            {
//                player.Id = value;
//                OnPropertyChanged();
//            }
//        }
//    }

//    public string PlayerName
//    {
//        get => player.Name;
//        set
//        {
//            if (player.Name != value)
//            {
//                player.Name = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(NameIsValid));
//                OnPropertyChanged(nameof(PlayerIsValid));

//                WeakReferenceMessenger.Default.Send(new PlayerNameChangedMessage(value));
//            }
//        }
//    }

//    public int PlayerInitialMoney
//    {
//        get => player.InitialMoney;
//        set
//        {
//            if (player.InitialMoney != value)
//            {
//                player.InitialMoney = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(InitialMoneyIsValid));
//                OnPropertyChanged(nameof(PlayerIsValid));

//                WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(value));
//            }
//        }
//    }

//    public bool PlayerIsInGame
//    {
//        get => player.IsInGame;
//        set
//        {
//            if (player.IsInGame != value)
//            {
//                player.IsInGame = value;
//                OnPropertyChanged();
//                OnPropertyChanged(nameof(PlayerIsValid));
//            }
//        }
//    }

//    public bool NameIsValid => (PlayerName == null) || (PlayerName?.Length <= maxNameLength);

//    public bool InitialMoneyIsValid => Helpers.ValueIsInRange(PlayerInitialMoney,
//        minInitialMoney, maxInitialMoney);

//    public bool PlayerIsValid
//    {
//        get
//        {
//            if (PlayerIsInGame)
//            {
//                return NameIsValid && InitialMoneyIsValid;
//            }
//            else
//            {
//                return true;
//            }
//        }
//    }

//    public PlayerSettingsViewModel()
//    {
//        player = new Player();
//    }
//}

