using System.Collections.Generic;

class Hand
{
   public List<Card> Cards{get;set;}

   public Hand()
   {
      Cards = new List<Card>();
   }

   //TODO: correctly adding aces...
   public int Points
   {
      get
      {
         var tot = 0;
         int aces = 0;
         foreach (var card in Cards)
         {
            var cardVal = card.Val >= 10 ? 10 : card.Val;
            if (cardVal == 1)
            {
               aces++;
               continue;
            }
            tot += cardVal;
         }

         for(int i=0; i<aces;i++)
         {
            if ((tot + 11) > 21)
            {
               tot += 1;
            }
            else
            {
               tot += 11;
            }
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