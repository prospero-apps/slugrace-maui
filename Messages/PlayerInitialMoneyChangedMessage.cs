using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Slugrace.Messages;

public class PlayerInitialMoneyChangedMessage : ValueChangedMessage<int?>
{
    public PlayerInitialMoneyChangedMessage(int? value) : base(value) { }
}
