using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class AccidentPopupViewModel : ObservableObject
{
    [ObservableProperty]
    private string affectedSlugImageUrl;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeadlineMessage))]
    private string affectedSlugName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HeadlineMessage))]
    private string accidentHeadline;

    public string HeadlineMessage => $"{AffectedSlugName} {AccidentHeadline}";

    public void ShowAccidentInfo(AccidentViewModel accidentViewModel)
    {
        AffectedSlugImageUrl = accidentViewModel.AffectedSlug.ImageUrl;
        AffectedSlugName = accidentViewModel.AffectedSlug.Name;
        AccidentHeadline = accidentViewModel.Headline;
    }
}
