namespace Slugrace.Models;

public enum EndingCondition
{
    Money,
    Races,
    Time
}

public class Game
{
    public List<Player> Players { get; set; }
    public List<Slug> Slugs { get; set;}
    public EndingCondition GameEndingCondition { get; set; }
    public int NumberOfRacesSet { get; set; }
    public int GameTimeSet { get; set; }
    public string GameOverReason { get; set; }
    public List<Player> Winners { get; set; }
    public int RaceNumber { get; set; }
    public int TimeElapsed { get; set; }
}
