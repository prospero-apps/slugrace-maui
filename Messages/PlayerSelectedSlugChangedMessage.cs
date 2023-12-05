using CommunityToolkit.Mvvm.Messaging.Messages;
using Slugrace.ViewModels;

namespace Slugrace.Messages;

public class PlayerSelectedSlugChangedMessage : ValueChangedMessage<SlugViewModel>
{
    public PlayerSelectedSlugChangedMessage(SlugViewModel value) : base(value) { }
}
