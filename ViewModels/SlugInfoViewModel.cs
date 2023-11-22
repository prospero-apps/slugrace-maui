using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class SlugInfoViewModel(GameManager gameManager, int slugId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Slug slug = gameManager.Slugs.Find(s => s.Id == slugId);

    public string SlugName => slug.Name;
    public int WinNumber => slug.WinNumber;
    public double Odds => slug.Odds;
}
