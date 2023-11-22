namespace Slugrace.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsInGame { get; set; }
    public int InitialMoney { get; set; } = 0;
    public int CurrentMoney { get; set; }
    public int Gain { get; set; }
    public int BetAmount { get; set; }

    //
    public Slug SelectedSlug { get; set; }
    public int PreviousMoney { get; set; }

}




// ***************************************************************************


//namespace Slugrace.Models;

//public class Player
//{
//    //const int maxNameLength = 10;
//    //const int minInitialMoney = 10;
//    //const int maxInitialMoney = 5000;

//    public int Id { get; set; }
//    public string Name { get; set; }

//    //private string name;
//    //public string Name
//    //{
//    //    get { return name; }
//    //    set
//    //    {
//    //        if (name != value)
//    //        {
//    //            name = value == string.Empty ? $"Player {Id}" : value;
//    //        }
//    //    }
//    //} 
//    public bool IsInGame { get; set; }
//    public int InitialMoney { get; set; } = 0;
//    public int CurrentMoney { get; set; }
//    public int Gain { get; set; }
//    public int BetAmount { get; set; }

//    //
//    public Slug SelectedSlug { get; set; }
//    public int PreviousMoney { get; set; }


//    //public bool NameIsValid => (Name == null) || (Name?.Length <= maxNameLength);
//    //public bool InitialMoneyIsValid => Helpers.ValueIsInRange(InitialMoney,
//    //    minInitialMoney, maxInitialMoney);
//    //public bool IsValid
//    //{
//    //    get
//    //    {
//    //        if (IsInGame)
//    //        {
//    //            return NameIsValid && InitialMoneyIsValid;
//    //        }
//    //        else
//    //        {
//    //            return true;
//    //        }
//    //    }
//    //}
//}



// **********************************************************

//namespace Slugrace.Models;

//public class Player
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public bool IsInGame { get; set; }
//    public int InitialMoney { get; set; } = 0;
//    public int CurrentMoney { get; set; }
//    public int Gain { get; set; }
//    public int BetAmount { get; set; }

//    //
//    public Slug SelectedSlug { get; set; }
//    public int PreviousMoney { get; set; }

//}
