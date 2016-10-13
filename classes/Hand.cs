using System.Collections.Generic;

class Hand
{
   public List<Card> Cards{get;set;}

   public Hand()
   {
      Cards = new List<Card>();
   }
   public int Points
   {
      get
      {
         var tot = 0;
         foreach (var card in Cards)
         {
            tot += card.Val;
         }
         return tot;
      }
   }
   public bool IsSoft 
   {
      get
      {
         //TODO: consider the case when ace in your hand doesn't mean
         //the hand is soft Ex. Ace + 9 + 9 = Hard 19
         foreach(var card in Cards)
         {
            if (card.Val == 1)
               return true;
         }
         
         return false;
      }
   }

   public bool HasBlackJack()
   {
      var res = false;
      if (Cards.Count != 2)
         return res;
      
      var hasAce = false;
      var hasTen = false;
      foreach (var card in Cards)
      {
         if (card.Val == 1)
            hasAce = true;
         if (card.Val >= 10)
            hasTen = true;
      }

      return hasAce && hasTen;
   }
}