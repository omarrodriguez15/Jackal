using System.Collections.Generic;

namespace Jackal
{
   public class Hand
   {
      public List<Card> Cards{get;set;} = new List<Card>();

      private bool _isSoft = false;

      public int Points
      {
         get
         {
            var tot = 0;
            int aces = 0;
            _isSoft = false;

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
                  _isSoft = true;
               }
            }
            return tot;
         }
      }

      public bool IsSoft 
      {
         get
         {
            //Calling the getter so the _isSoft flag is updated if it needs to be
            //being lazy and not extracting logic to check if a ace is being used
            //as an 11
            var unused = Points;
            return _isSoft;
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
}