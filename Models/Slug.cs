namespace Slugrace.Models;

public class Slug
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Odds { get; set; }
    public double PreviousOdds { get; set; }
    public int WinNumber { get; set; }
    public string ImageUrl { get; set; }
}
