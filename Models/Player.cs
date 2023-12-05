namespace Slugrace.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public int InitialMoney { get; set; } = 0;
    public int CurrentMoney { get; set; }    
    public int WonOrLostMoney { get; set; }
    public int BetAmount { get; set; }
    public bool IsInGame { get; set; }    
    public int PreviousMoney { get; set; }
    public bool IsBankrupt { get; set; }
}