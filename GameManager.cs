using Slugrace.Models;

namespace Slugrace;

public enum EndingCondition
{
    Money,
    Races,
    Time
}

public class GameManager
{
    public List<Player> Players { get; set; }
    public List<Slug> Slugs { get; set; }
    public EndingCondition EndingCondition { get; set; } = EndingCondition.Money;
    public int NumberOfRacesSet { get; set; } = 0;
    public int GameTimeSet { get; set; } = 0;

    //
    public bool GameIsOver { get; set; } = false;
    public string GameOverReason { get; set; }
    public List<Player> Winners { get; set; }
    public int RaceNumber { get; set; }
    public int TimeElapsed { get; set; }

    public GameManager()
    {
        Players = new List<Player>();
        Slugs = new List<Slug>()
        {
            new Slug { Name = "Speedster" },
            new Slug { Name = "Trusty" },
            new Slug { Name = "Iffy" },
            new Slug { Name = "Slowpoke" }
        };
    }
}








// *********************************************************************

//using Slugrace.Models;

//namespace Slugrace;

//public enum EndingCondition
//{
//    Money,
//    Races,
//    Time
//}

//public class GameManager
//{
//    public List<Player> Players { get; set; }
//    public List<Slug> Slugs { get; set; }
//    public EndingCondition EndingCondition { get; set; } = EndingCondition.Money;
//    public int NumberOfRacesSet { get; set; } = 0;
//    public int GameTimeSet { get; set; } = 0;

//    //
//    public bool GameIsOver { get; set; } = false;
//    public string GameOverReason { get; set; }
//    public List<Player> Winners { get; set; }
//    public int RaceNumber { get; set; }
//    public int TimeElapsed { get; set; }

//    public GameManager()
//    {
//        Players = new List<Player>();
//        Slugs = new List<Slug>()
//        {
//            new Slug { Name = "Speedster" },
//            new Slug { Name = "Trusty" },
//            new Slug { Name = "Iffy" },
//            new Slug { Name = "Slowpoke" }
//        };
//    }
//}

// **********************************************************

//using Slugrace.Models;

//namespace Slugrace;

//public enum EndingCondition
//{
//    Money,
//    Races,
//    Time
//}

//public class GameManager
//{
//    public List<Player> Players { get; set; }
//    public List<Slug> Slugs { get; set; }
//    public EndingCondition EndingCondition { get; set; } = EndingCondition.Money;
//    public int NumberOfRacesSet { get; set; } = 0;
//    public int GameTimeSet { get; set; } = 0;

//    //
//    public bool GameIsOver { get; set; } = false;
//    public string GameOverReason { get; set; }
//    public List<Player> Winners { get; set; }
//    public int RaceNumber { get; set; }
//    public int TimeElapsed { get; set; }

//    public GameManager()
//    {
//        Players = new List<Player>();
//        Slugs = new List<Slug>()
//        {
//            new Slug { Name = "Speedster" },
//            new Slug { Name = "Trusty" },
//            new Slug { Name = "Iffy" },
//            new Slug { Name = "Slowpoke" }
//        };
//    }
//}

