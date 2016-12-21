using System.Collections.Generic;
using System.Diagnostics;

namespace Jackal
{
   public class AutoBasicStrategy : IBlackJackStrategy
   {
      private Dictionary<int, Dictionary<int, char>> _hardMoves = new Dictionary<int, Dictionary<int, char>>()
      {
         //TODO:
         //This is a a pair of 2's should split but for now hitting until split logic is figured out
         {4, _alwaysHit},
         {5, _alwaysHit},
         {6, _alwaysHit},
         {7, _alwaysHit},
         {8, _alwaysHit},
         {9, new Dictionary<int, char>(){{2, 'h'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {10, new Dictionary<int, char>(){{2, 'd'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'd'},{8, 'd'},{9, 'd'},{10, 'h'},{1, 'h'}}},
         {11, new Dictionary<int, char>(){{2, 'd'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'd'},{8, 'd'},{9, 'd'},{10, 'd'},{1, 'h'}}},
         {12, new Dictionary<int, char>(){{2, 'h'},{3, 'h'},{4, 's'},{5, 's'},{6, 's'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {13, new Dictionary<int, char>(){{2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 's'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {14, new Dictionary<int, char>(){{2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 's'},{7, 'h'},{8, 'h'},{9, 's'},{10, 'h'},{1, 'h'}}},
         {15, new Dictionary<int, char>(){{2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 's'},{7, 's'},{8, 's'},{9, 's'},{10, 's'},{1, 's'}}},
         {16, new Dictionary<int, char>(){{2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 's'},{7, 's'},{8, 's'},{9, 's'},{10, 's'},{1, 's'}}},
         {17, _alwaysStand},
         {18, _alwaysStand},
         {19, _alwaysStand},
         {20, _alwaysStand},
         {21, _alwaysStand}
      };

      private Dictionary<int, Dictionary<int, char>> _softMoves = new Dictionary<int, Dictionary<int, char>>()
      {
         //Example hand is Ace, 2 = soft 13 => 13 % 10 = 3
         {3, new Dictionary<int, char>(){{2, 'h'},{3, 'h'},{4, 'h'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {4, new Dictionary<int, char>(){{2, 'h'},{3, 'h'},{4, 'h'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {5, new Dictionary<int, char>(){{2, 'h'},{3, 'h'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {6, new Dictionary<int, char>(){{2, 'h'},{3, 'h'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {7, new Dictionary<int, char>(){{2, 'h'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {8, new Dictionary<int, char>(){{2, 'd'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 's'},{8, 's'},{9, 'h'},{10, 'h'},{1, 'h'}}},
         {9, new Dictionary<int, char>(){{2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 'd'},{7, 's'},{8, 's'},{9, 's'},{10, 's'},{1, 's'}}},
         //this is a soft 20 situation
         {0, _alwaysStand},
         //this is a soft 21 situation
         {1,_alwaysStand},
         //TODO: this is a soft 12 situation needs to be handled with split logic 
         //b/c it is a pair of A's but for now we will treat as a hard 12
         {2, new Dictionary<int, char>(){{2, 'd'},{3, 'd'},{4, 'd'},{5, 'd'},{6, 'd'},{7, 's'},{8, 's'},{9, 'h'},{10, 'h'},{1, 'h'}}},
      };

      private static readonly Dictionary<int, char> _alwaysStand = new Dictionary<int, char>()
      {
         {2, 's'},{3, 's'},{4, 's'},{5, 's'},{6, 's'},{7, 's'},{8, 's'},{9, 's'},{10, 's'},{1, 's'}
      };
      
      private static readonly Dictionary<int, char> _alwaysHit = new Dictionary<int, char>()
      {
         {2, 'h'},{3, 'h'},{4, 'h'},{5, 'h'},{6, 'h'},{7, 'h'},{8, 'h'},{9, 'h'},{10, 'h'},{1, 'h'}
      };
      public bool HasNextMove(Hand hand, Card dealerUpCard, ref char NextMove)
      {
         if(hand.HasBlackJack())
               return false;

         if(!hand.IsSoft)
         {
               var playerIndex = hand.Points;
               Debug.Assert(playerIndex >= 4 && playerIndex <= 21);
               Debug.Assert(dealerUpCard.Val >= 1 && dealerUpCard.Val <= 10);

               NextMove = _hardMoves[hand.Points][dealerUpCard.Val];
         }
         else
         {
               var playerIndex = hand.Points % 10;
               Debug.Assert(playerIndex >= 0 && playerIndex <= 9);
               Debug.Assert(dealerUpCard.Val >= 1 && dealerUpCard.Val <= 10);

               NextMove = _softMoves[playerIndex][dealerUpCard.Val];
         }

         return NextMove == 's' ? false : true;
      }
   }
}