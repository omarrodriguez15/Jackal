using System;
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
         //Console.Out.WriteLine($"round: {i+1}");
         DealCards();
         //Let each player make their moves
         foreach (var player in Players)
         {
            var playerHand = $"c0:{player.hand.Cards[0].ValString};c1:{player.hand.Cards[1].ValString};pts:{player.hand.Points};";
            //Console.Out.WriteLine(playerHand);
            player.cash -= player.bet;
            while (player.Move())
            {
               //Console.Out.WriteLine($"pm:{player.NextMove}");
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
               playerHand = $"c0:{player.hand.Cards[0].ValString};c1:{player.hand.Cards[1].ValString};pts:{player.hand.Points};";
               //Console.Out.WriteLine(playerHand);
            }
         }
         var dealerHand = $"d0:{dealer.hand.Cards[0].ValString};d1:{dealer.hand.Cards[1].ValString};pts:{dealer.hand.Points};";
         //Console.Out.WriteLine(dealerHand);
         //Dealers turn to make his moves
         while (dealer.Move())
         {
            //Console.Out.WriteLine($"dm:{dealer.NextMove}");
            if (dealer.NextMove == 'h')
            {
               dealer.hand.Cards.Add(mainDeck.cards.Pop());
            }
            dealerHand = $"dpts:{dealer.hand.Points};";
            //Console.Out.WriteLine(dealerHand);
         }
         //Splitting the following into two
         //methods makes it loop over the players twice
         //but I think makes the code simpler...
         DeterminePlayerStatuses();
         PayPlayers();
         DisposeOfCards();
      }
   }

    private void DisposeOfCards()
    {
        foreach(var player in Players)
           player.hand.Cards.Clear();
    }

    private void PayPlayers()
    {
        foreach (var player in Players)
        {
            //Console.Out.WriteLine($"player status: {player.Status};Cash: {player.cash};");
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
            //Console.Out.WriteLine($"playerCashAfter: {player.cash}");
        }
    }

   private void DeterminePlayerStatuses()
   {
      var dealerPoints = dealer.hand.Points;
      var dealerBusted = dealerPoints > 21;
      //Console.Out.WriteLine($"dPts: {dealerPoints}");

      foreach (var player in Players)
      {
         //Console.Out.WriteLine($"pPts: {player.hand.Points}");
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