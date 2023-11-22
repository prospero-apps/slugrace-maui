using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class WinnerInfoViewModel(GameManager gameManager, int slugId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Slug slug = gameManager.Slugs.Find(s => s.Id == slugId);

    public string SlugName => slug.Name;
    public string SlugImage => slug.ImageUrl;
}
