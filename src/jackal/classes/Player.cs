namespace Jackal
{
   public class Player
   {
      public string Name = "";
      public int cash = 1000;
      public int bet = 5;
      public int BaseBet = 5;
      public bool wonLastHand = true;
      public IBettingStrategy BettingStrategy = new BoringBetting();
      public IBlackJackStrategy PlayingStrategy = new ManualBasicStrategy();
      //h = hit,s = stand, d = Double, y = split, n = don't split
      public char NextMove = ' ';
      public Hand hand { get; set;}
      public Dealer dealer {get;set;}
      public char Status = 'n';

      public Player(string name = "default")
      {
         hand = new Hand();
         Name = name;
      }

      public bool Move()
      {
         return PlayingStrategy.HasNextMove(hand, dealer.hand.Cards[0], ref NextMove);
      }

      //TODO: Case where we don't have any money left 
      public int GetNewBet()
      {
         return BettingStrategy.GetNextBet(BaseBet, bet, Status);
      }
   }
}