class Hand
{
   public List<Card> Cards{get;set;}
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
}