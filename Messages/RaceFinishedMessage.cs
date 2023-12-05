using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Slugrace.Messages;

public class RaceFinishedMessage : ValueChangedMessage<RaceStatus>
{
    public RaceFinishedMessage(RaceStatus value) : base(value) { }
}
