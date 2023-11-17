using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Slugrace.Messages;

public class PlayerNameChangedMessage : ValueChangedMessage<string>
{
    public PlayerNameChangedMessage(string value) : base(value)
    {

    }
}
