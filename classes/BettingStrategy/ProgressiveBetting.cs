namespace Jackal
{
   public class ProgressiveBetting : IBettingStrategy
   {
      public int GetNextBet(int baseBet, int currentBet, char status)
      {
         if (status == 'w')
         {
               return (int)(currentBet * 1.5);
         }

         return baseBet;
      }
   }
}