using CommunityToolkit.Mvvm.ComponentModel;

namespace Slugrace.ViewModels;

public partial class SlugsStatsViewModel : ObservableObject
{
    private GameManager gameManager;
    private List<SlugStatsViewModel> slugs;

    public SlugsStatsViewModel(GameManager gameManager)
    {
        this.gameManager = gameManager;

        foreach (var slug in gameManager.Slugs)
        {
            slugs.Add(new SlugStatsViewModel(gameManager, slug.Id));
        }
    }
}
