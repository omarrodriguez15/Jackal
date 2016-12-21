namespace Jackal
{
   public class BoringBetting : IBettingStrategy
   {
      public int GetNextBet(int baseBet, int currentBet, char status)
      {
         return baseBet;
      }
   }
}