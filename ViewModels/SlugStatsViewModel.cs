using CommunityToolkit.Mvvm.ComponentModel;
using Slugrace.Models;

namespace Slugrace.ViewModels;

public partial class SlugStatsViewModel(GameManager gameManager, int slugId) : ObservableObject
{
    private GameManager gameManager = gameManager;
    private Slug slug = gameManager.Slugs.Find(s => s.Id == slugId);

    public string SlugName => slug.Name;
    public int SlugWinNumber => slug.WinNumber;
    public double SlugWinPercentage => Math.Round((double)(SlugWinNumber / gameManager.RaceNumber * 100), 2);
}
