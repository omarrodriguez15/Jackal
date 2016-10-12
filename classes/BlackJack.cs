using System.Collections.Generic;

class BlackJack
{
   public int AmtOfDecks {get;set;}
   public List<Player> Players {get;set;}
   public Dealer dealer {get;set;}
   private Deck mainDeck = null;
   private Stack<Card> discardDeck = null;
   public BlackJack(int amtOfDecks, int players)
   {
      AmtOfDecks = amtOfDecks;
      mainDeck = new Deck(amtOfDecks);
      Players = new List<Player>(players);
   }

   public void DealHands()
   {
      Card card;

      foreach(var player in Players)
      {
         card = mainDeck.cards.Pop();
         player.Hand.Cards.Add(card);
         discardDeck.Push(card);

         card = mainDeck.cards.Pop();
         player.Hand.Cards.Add(card);
         discardDeck.Push(card);
      }
      card = mainDeck.cards.Pop();
      dealer.Hand.Cards.Add(card);
      discardDeck.Push(card);

      card = mainDeck.cards.Pop();
      dealer.Hand.Cards.Add(card);
      discardDeck.Push(card);
      //If dealer has black jack notify someone...

   }


}