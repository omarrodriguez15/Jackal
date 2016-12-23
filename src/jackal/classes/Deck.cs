using System;
using System.Collections.Generic;

namespace Jackal
{
   public class Deck
   {
      public Stack<Card> cards = new Stack<Card>();

      private int numOfDecks;
      public Deck(int amtOfDecks)
      {
         this.numOfDecks = amtOfDecks;
         ShuffleCards();
      }

      public void ShuffleCards()
      {
         var cardTotal = 52 * numOfDecks;
         var collisions = 0;
         Card[] tempCards = new Card[cardTotal];

         for (int i=0; i < numOfDecks ;i++)
         {
            List<Card> OrderedCards = Card.GetOrderedCards();
            var ran = new Random(DateTime.Now.Millisecond * DateTime.UtcNow.Millisecond);

            foreach(var card in OrderedCards)
            {
               //Get a random index in the range of the full deck
               var ndx = ran.Next(0, tempCards.Length);
               
               //Keep looking for an index until we find a
               //blank spot in the deck
               while (tempCards[ndx] != null)
               {
                  ndx = ran.Next(0, tempCards.Length);
                  collisions++;
               }

               tempCards[ndx] = card;
            }
         }

         CustomLogger.Log($"Generating the deck caused {collisions} collisions");
         cards = new Stack<Card>(tempCards);
      }
   }
}