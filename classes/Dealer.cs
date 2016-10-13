class Dealer
{
   public int SoftHitNumber = 16;
   public Hand hand{get;set;}
   public char NextMove = ' ';

   public Dealer()
   {
      hand = new Hand();
   }
   public bool Move()
   {
      if (hand.Points < SoftHitNumber)
      {
         NextMove = 'h';
         return true;
      }
      //Hit on soft 16 by default
      if (hand.IsSoft && hand.Points <= SoftHitNumber)
      {
         NextMove = 'h';
         return true;
      }
      return false;
   }
}