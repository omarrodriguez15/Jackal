using System.Collections.Generic;

namespace Jackal
{
   public class Card
   {
      public int Suit = -1;
      public int Val = -1;
      public bool HasFace = false;

      public string SuitString 
      {
         get
         {
            switch (Suit)
            {
               case 0: return "spade";
               case 1: return "heart";
               case 2: return "diamond";
               case 3: return "clover";
               default: return "JOKER!!!!!";
            }
         }
      }
      public string ValString 
      {
         get
         {
            switch (Val)
            {
               case 1: return "ace";
               case 11: return "jack";
               case 12: return "queen";
               case 13: return "king";
               default: return Val.ToString();
            }
         }
      }

      private static List<Card> OrderedCards = null;

      public Card(int suit, int val, bool hasFace = false)
      {
         Suit = suit;
         Val = val;
         HasFace = hasFace;
      }

      public static List<Card> GetOrderedCards()
      {
         if (OrderedCards != null)
            return OrderedCards;

         OrderedCards = new List<Card>();

         //Forech suit
         for (int suit=0; suit < 4 ;suit++)
         {
            //Ace through King 1-13
            for (int val=1; val <= 13 ;val++)
            {
               //val is 1 or greater than 10 its a face card
               var isFaceCard = val == 1 ? true : val > 10 ? true : false;
               OrderedCards.Add(new Card(suit, val, isFaceCard));
            }
         }
         
         return OrderedCards;
      }
   }
}