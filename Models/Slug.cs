namespace Slugrace.Models;

public class Slug
{
    public string Name { get; set; }
    public double Odds { get; set; }
    public double PreviousOdds { get; set; }
    public int WinNumber { get; set; }
    public string ImageUrl { get; set; }
    public string EyeImageUrl { get; set; }
    public string BodyImageUrl { get; set; }
    public double BaseOdds { get; set; }
    public bool IsRaceWinner { get; set; }
    public string WinSound {  get; set; }
}
