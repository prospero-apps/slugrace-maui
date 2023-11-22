using Slugrace.ViewModels;

namespace Slugrace.Controls;

public partial class PlayerSettings : ContentView
{
    public PlayerSettings()
    {
        InitializeComponent();
    }

    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
    {
        bool nameValid = (BindingContext as PlayerSettingsViewModel).NameIsValid;
        GoToNameState(nameValid);
    }

    private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext != null)
        {
            bool initialMoneyValid = (BindingContext as PlayerSettingsViewModel).InitialMoneyIsValid;
            Helpers.HandleNumericEntryState(initialMoneyValid, initialMoneyEntry);
        }
    }

    void GoToNameState(bool nameValid)
    {
        string visualState = nameValid ? "Valid" : "Invalid";
        VisualStateManager.GoToState(nameEntry, visualState);
    }
}


// ***************************************************************************

//using CommunityToolkit.Mvvm.Messaging;
//using Slugrace.Messages;
//using Slugrace.ViewModels;

//namespace Slugrace.Controls;

//public partial class PlayerSettings : ContentView
//{
//    public PlayerSettings()
//    {
//        InitializeComponent();
//    }

//    const int maxNameLength = 10;
//    const int minInitialMoney = 10;
//    const int maxInitialMoney = 5000;

//    // properties

//    // PlayerId
//    public static readonly BindableProperty PlayerIdProperty = BindableProperty.Create(
//        nameof(PlayerId),
//        typeof(int),
//        typeof(PlayerSettings),
//        null,
//        propertyChanged: OnPlayerIdChanged);

//    public int PlayerId
//    {
//        get => (int)GetValue(PlayerIdProperty);
//        set => SetValue(PlayerIdProperty, value);
//    }

//    private static void OnPlayerIdChanged(BindableObject bindable, object oldValue, object newValue)
//    {
//        var player = (PlayerSettings)bindable;
//        player.OnPropertyChanged();
//    }

//    // PlayerName
//    public static readonly BindableProperty PlayerNameProperty = BindableProperty.Create(
//        nameof(PlayerName),
//        typeof(string),
//        typeof(PlayerSettings),
//        string.Empty,
//        propertyChanged: OnPlayerNameChanged);

//    public string PlayerName
//    {
//        get => (string)GetValue(PlayerNameProperty);
//        set => SetValue(PlayerNameProperty, value);
//    }

//    private static void OnPlayerNameChanged(BindableObject bindable, object oldValue, object newValue)
//    {
//        var player = (PlayerSettings)bindable;
//        player.OnPropertyChanged();
//        player.OnPropertyChanged(nameof(NameIsValid));
//        player.OnPropertyChanged(nameof(PlayerIsValid));

//        WeakReferenceMessenger.Default.Send(new PlayerNameChangedMessage(player.PlayerName));
//    }


//    // PlayerInitialMoney
//    public static readonly BindableProperty PlayerInitialMoneyProperty = BindableProperty.Create(
//        nameof(PlayerInitialMoney),
//        typeof(int),
//        typeof(PlayerSettings),
//        null,
//        propertyChanged: OnPlayerInitialMoneyChanged);

//    public int PlayerInitialMoney
//    {
//        get => (int)GetValue(PlayerInitialMoneyProperty);
//        set => SetValue(PlayerInitialMoneyProperty, value);
//    }

//    private static void OnPlayerInitialMoneyChanged(BindableObject bindable, object oldValue, object newValue)
//    {
//        var player = (PlayerSettings)bindable;               

//        player.OnPropertyChanged();
//        player.OnPropertyChanged(nameof(InitialMoneyIsValid));
//        player.OnPropertyChanged(nameof(PlayerIsValid));

//        WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(player.PlayerInitialMoney));
//    }

//    //public static readonly BindableProperty PlayerInitialMoneyProperty = BindableProperty.Create(
//    //    nameof(PlayerInitialMoney),
//    //    typeof(int),
//    //    typeof(PlayerSettings),
//    //    null);

//    //public int PlayerInitialMoney
//    //{
//    //    get => (int)GetValue(PlayerInitialMoneyProperty);
//    //    set
//    //    {
//    //        SetValue(PlayerInitialMoneyProperty, value);
//    //        OnPropertyChanged();
//    //        OnPropertyChanged(nameof(InitialMoneyIsValid));
//    //        OnPropertyChanged(nameof(PlayerIsValid));

//    //        WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(value));
//    //    }
//    //}

//    //private static void OnPlayerInitialMoneyChanged(BindableObject bindable, object oldValue, object newValue)
//    //{
//    //    var player = (PlayerSettings)bindable;
//    //    player.OnPropertyChanged();
//    //    player.OnPropertyChanged(nameof(InitialMoneyIsValid));
//    //    player.OnPropertyChanged(nameof(PlayerIsValid));

//    //    WeakReferenceMessenger.Default.Send(new PlayerInitialMoneyChangedMessage(player.PlayerInitialMoney));
//    //}

//    // PlayerIsInGame
//    public static readonly BindableProperty PlayerIsInGameProperty = BindableProperty.Create(
//        nameof(PlayerIsInGame),
//        typeof(bool),
//        typeof(PlayerSettings),
//        false,
//        propertyChanged: OnPlayerIsInGameChanged);

//    public bool PlayerIsInGame
//    {
//        get => (bool)GetValue(PlayerIsInGameProperty);
//        set => SetValue(PlayerIsInGameProperty, value);
//    }

//    private static void OnPlayerIsInGameChanged(BindableObject bindable, object oldValue, object newValue)
//    {
//        var player = (PlayerSettings)bindable;
//        player.OnPropertyChanged();
//        player.OnPropertyChanged(nameof(PlayerIsValid));
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



//    //// PlayerNameIsValid
//    //public static readonly BindableProperty PlayerNameIsValidProperty = BindableProperty.Create(
//    //    nameof(PlayerNameIsValid),
//    //    typeof(bool),
//    //    typeof(PlayerSettings),
//    //    false,
//    //    propertyChanged: OnPlayerNameIsValidChanged);

//    //public bool PlayerNameIsValid
//    //{
//    //    get => (bool)GetValue(PlayerNameIsValidProperty);
//    //    set => SetValue(PlayerNameIsValidProperty, value);
//    //}

//    //private static void OnPlayerNameIsValidChanged(BindableObject bindable, object oldValue, object newValue)
//    //{
//    //    var player = (PlayerSettings)bindable;
//    //    player.OnPropertyChanged();
//    //    player.OnPropertyChanged(nameof(PlayerIsValid));
//    //}

//    //// PlayerInitialMoneyIsValid
//    //public static readonly BindableProperty PlayerInitialMoneyIsValidProperty = BindableProperty.Create(
//    //    nameof(PlayerInitialMoneyIsValid),
//    //    typeof(bool),
//    //    typeof(PlayerSettings),
//    //    false,
//    //    propertyChanged: OnPlayerIsInGameChanged);

//    //public bool PlayerInitialMoneyIsValid
//    //{
//    //    get => (bool)GetValue(PlayerInitialMoneyIsValidProperty);
//    //    set => SetValue(PlayerInitialMoneyIsValidProperty, value);
//    //}

//    //private static void OnPlayerInitialMoneyIsValidChanged(BindableObject bindable, object oldValue, object newValue)
//    //{
//    //    var player = (PlayerSettings)bindable;
//    //    player.OnPropertyChanged();
//    //    player.OnPropertyChanged(nameof(PlayerIsValid));
//    //}

//    //// PlayerIsValid
//    //public static readonly BindableProperty PlayerIsValidProperty = BindableProperty.Create(
//    //    nameof(PlayerIsValid),
//    //    typeof(bool),
//    //    typeof(PlayerSettings),
//    //    false,
//    //    propertyChanged: OnPlayerIsInGameChanged);

//    //public bool PlayerIsValid
//    //{
//    //    get => (bool)GetValue(PlayerIsValidProperty);
//    //    set => SetValue(PlayerIsValidProperty, value);
//    //}

//    //private static void OnPlayerIsValidChanged(BindableObject bindable, object oldValue, object newValue)
//    //{
//    //    var player = (PlayerSettings)bindable;
//    //    player.OnPropertyChanged();
//    //}



//    // other properties
//    //public bool NameIsValid => (PlayerName == null) || (PlayerName?.Length <= maxNameLength);

//    //public bool InitialMoneyIsValid => Helpers.ValueIsInRange(PlayerInitialMoney,
//    //    minInitialMoney, maxInitialMoney);

//    //public bool PlayerIsValid
//    //{
//    //    get
//    //    {
//    //        if (PlayerIsInGame)
//    //        {
//    //            return NameIsValid && InitialMoneyIsValid;
//    //        }
//    //        else
//    //        {
//    //            return true;
//    //        }
//    //    }
//    //}




//    //private void OnNameTextChanged(object sender, TextChangedEventArgs e)
//    //{
//    //    bool nameValid = (BindingContext as PlayerSettingsViewModel).NameIsValid;
//    //    GoToNameState(nameValid);
//    //}

//    //private void OnNameTextChanged(object sender, TextChangedEventArgs e)
//    //{
//    //    GoToNameState(NameIsValid);
//    //}
//    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
//    {
//        GoToNameState(NameIsValid);
//    }

//    //private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
//    //{
//    //    if (BindingContext != null)
//    //    {
//    //        bool initialMoneyValid = (BindingContext as PlayerSettingsViewModel).InitialMoneyIsValid;
//    //        Helpers.HandleNumericEntryState(initialMoneyValid, initialMoneyEntry);
//    //    }
//    //}

//    //private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
//    //{        
//    //    Helpers.HandleNumericEntryState(InitialMoneyIsValid, initialMoneyEntry);
//    //}
//    private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
//    {
//        Helpers.HandleNumericEntryState(InitialMoneyIsValid, initialMoneyEntry);
//    }

//    void GoToNameState(bool nameValid)
//    {
//        string visualState = nameValid ? "Valid" : "Invalid";

//        if (nameEntry != null)
//        {
//            VisualStateManager.GoToState(nameEntry, visualState);
//        }        
//    }
//}

// ****************************************************************************

//using Slugrace.ViewModels;

//namespace Slugrace.Controls;

//public partial class PlayerSettings : ContentView
//{
//    public PlayerSettings()
//    {
//        InitializeComponent();
//    }

//    private void OnNameTextChanged(object sender, TextChangedEventArgs e)
//    {
//        bool nameValid = (BindingContext as PlayerSettingsViewModel).NameIsValid;
//        GoToNameState(nameValid);
//    }

//    private void OnInitialMoneyTextChanged(object sender, TextChangedEventArgs e)
//    {
//        if (BindingContext != null)
//        {
//            bool initialMoneyValid = (BindingContext as PlayerSettingsViewModel).InitialMoneyIsValid;
//            Helpers.HandleNumericEntryState(initialMoneyValid, initialMoneyEntry);
//        }
//    }

//    void GoToNameState(bool nameValid)
//    {
//        string visualState = nameValid ? "Valid" : "Invalid";
//        VisualStateManager.GoToState(nameEntry, visualState);
//    }
//}

