namespace Jackal
{
   public class ManualBasicStrategy : IBlackJackStrategy
   {
      public bool HasNextMove(Hand hand, Card dealerUpCard, ref char NextMove)
      {
         //Blackjack!
         if (hand.HasBlackJack())
         {
            return false;
         }

         //var dealerUpCard = dealer.hand.Cards[0];
         //Soft hand
         if (hand.IsSoft)
         {
            //Soft 13-15
            if (hand.Points <= 15)
            {
               NextMove = 'h';
               return true;
            }
            //Soft 16-18
            if (hand.Points <= 18)
            {
               //Dealer up card is 2-6
               if (dealerUpCard.Val <= 6 && dealerUpCard.Val != 1)
                  NextMove = 'd';
               //Dealer up card is 7-A
               else if (dealerUpCard.Val <= 13 || dealerUpCard.Val == 1)
                  NextMove = 'h';
               
               return true;
            }
            //Soft 19-21
            if (hand.Points <= 21)
            {
               NextMove = 's';
               return false;
            }
               
            //Busted!
            return false;
         }
         //Hard hand (No aces)
         else
         {
            //Hard 4-8
            if (hand.Points <= 8)
            {
               NextMove = 'h';
               return true;
            }
            //Hard 9
            if (hand.Points == 9)
            {
               //2-6
               if (dealerUpCard.Val <= 6 && dealerUpCard.Val != 1)
                  NextMove = 'd';
               //7-A
               else if (dealerUpCard.Val <= 13 || dealerUpCard.Val == 1)
                  NextMove = 'h';
               return true;
            }
            //10 or 11 d if higher than dealer
            if ((hand.Points == 10 || hand.Points == 11) && dealerUpCard.Val < hand.Points && dealerUpCard.Val != 1)
            {
               NextMove = 'd';
               return true;
            }
               
            //12-16
            if (hand.Points <= 16)
            {
               //Stand on 2-6
               if (dealerUpCard.Val <= 6 && dealerUpCard.Val != 1)
               {
                  NextMove = 's';
                  return false;
               }
                  
               //Hit on 7-A
               if (dealerUpCard.Val <= 13 || dealerUpCard.Val == 1)
                  NextMove = 'h';
               //Console.Out.WriteLine($"duc:{dealerUpCard.Val};NextMove:{NextMove};");
               return true;
            }
               
            //Stand on 17-21
            if (hand.Points <= 21)
            {
               NextMove = 's';
               return false;
            }
            //Busted
            return false;
         }
      }
   }
}