namespace Jackal
{
   public interface IBettingStrategy
   {
      int GetNextBet(int baseBet, int currentBet, char status);
   }
}