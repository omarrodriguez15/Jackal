using System.Collections.Generic;

class BlackJack
{
   public int AmtOfDecks {get;set;}
   public List<Player> Players {get;set;}
   public Dealer dealer {get;set;}
   private Deck mainDeck = null;
   public BlackJack(int amtOfDecks, int players)
   {
      AmtOfDecks = amtOfDecks;
      dealer = new Dealer();
      mainDeck = new Deck(amtOfDecks);
      Players = new List<Player>();

      for (int i=0; i<players;i++)
      {
          Players.Add(new Player());
      }
   }

   public void SimulateRounds(int rounds)
   {
      for (int i=0; i < rounds; i++)
      {
         DealCards();
         //Let each player make their moves
         foreach (var player in Players)
         {
            player.cash -= player.bet;
            while (player.Move())
            {
               switch(player.NextMove)
               {
                  case 'h'://hit
                     player.hand.Cards.Add(mainDeck.cards.Pop());
                     break;
                  case 's'://stand
                     break;
                  case 'd'://double
                     player.hand.Cards.Add(mainDeck.cards.Pop());
                     break;
                  case 'y'://split
                     break;
                  default:
                     break;
               }
            }
         }
         
         //Dealers turn to make his moves
         while (dealer.Move())
         {
            if (dealer.NextMove == 'h')
            {
               dealer.hand.Cards.Add(mainDeck.cards.Pop());
            }
         }
         //Splitting the following into two
         //methods makes it loop over the players twice
         //but I think makes the code simpler...
         DeterminePlayerStatuses();
         PayPlayers();
      }
   }

    private void PayPlayers()
    {
        foreach (var player in Players)
        {
            switch(player.Status)
            {
               case 'b'://Bust
               case 'l'://Lost
                  break;
               case 'p'://Push
                  player.cash += player.bet;
                  break;
               case 'j'://BlackJack
                  player.cash += (int)(player.bet * 1.5);
                  break;
               case 'w'://win
                  player.cash += player.bet * 2;
                  break;
               default:
                  break;
            }
        }
    }

   private void DeterminePlayerStatuses()
   {
      var dealerPoints = dealer.hand.Points;
      var dealerBusted = dealerPoints > 21;
      foreach (var player in Players)
      {
         //Player busted
         if (player.hand.Points > 21)
         {
            player.Status = 'b';
            continue;
         }

         //Dealer busted
         if (dealerBusted)
         {
            //BlackJack
            if (player.hand.HasBlackJack())
            {
               player.Status = 'j';
               continue;
            }

            player.Status = 'w';
            continue;
         }

         //Push
         if (player.hand.Points == dealerPoints)
         {
            player.Status = 'p';
            continue;
         }

         //BlackJack
         if (player.hand.HasBlackJack())
         {
            player.Status = 'j';
            continue;
         }

         //Beat dealer
         if (player.hand.Points > dealerPoints)
         {
            player.Status = 'w';
            continue;
         }
      
         player.Status = 'l';
      }
   }
   public void DealCards()
   {
      if (mainDeck.cards.Count <= ((AmtOfDecks * 52) * .5))
         mainDeck.ShuffleCards();
      foreach(var player in Players)
      {
         player.dealer = dealer;
         player.hand.Cards.Add(mainDeck.cards.Pop());
         player.hand.Cards.Add(mainDeck.cards.Pop());
      }

      dealer.hand.Cards.Add(mainDeck.cards.Pop());
      dealer.hand.Cards.Add(mainDeck.cards.Pop());
      //If dealer has black jack notify someone...
   }


}