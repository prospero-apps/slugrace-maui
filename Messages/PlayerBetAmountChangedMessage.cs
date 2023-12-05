using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Slugrace.Messages;

public class PlayerBetAmountChangedMessage : ValueChangedMessage<int>
{
    public PlayerBetAmountChangedMessage(int value) : base(value) { }
}
