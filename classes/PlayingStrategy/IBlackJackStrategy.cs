namespace Jackal
{
   public interface IBlackJackStrategy
   {
      bool HasNextMove(Hand hand, Card dealerUpCard, ref char NextMove);
   }
}