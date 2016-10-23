using System;
using System.Collections.Generic;
using System.Text;

class BlackJack
{
   public int AmtOfDecks {get;set;}
   public List<Player> Players {get;set;}
   public Dealer dealer {get;set;}
   public Deck mainDeck = null;
   public bool boringBet = true;
   public StringBuilder sb = new StringBuilder();

   public BlackJack(int amtOfDecks, int players, int startingAmt = 1000, int betAmt = 5)
   {
      AmtOfDecks = amtOfDecks;
      dealer = new Dealer();
      mainDeck = new Deck(amtOfDecks);
      Players = new List<Player>();

      for (int i=0; i<players;i++)
      {
          Players.Add(new Player($"Player#{i+1}"){ cash = startingAmt, bet = betAmt });
      }
   }

   public void SimulateRounds(int rounds)
   {
      for (int i=0; i < rounds; i++)
      {
         Console.Out.WriteLine($"round: {i+1}");
         sb.AppendLine($"round: {i+1}");

         DealCards();
         //Let each player make their moves
         foreach (var player in Players)
         {
            var playerHand = $"{player.Name}:Card:{player.hand.Cards[0].ValString};Card:{player.hand.Cards[1].ValString};pts:{player.hand.Points};";
            Console.Out.WriteLine(playerHand);
            sb.AppendLine(playerHand);

            player.bet = GetNewBet(player);
            player.cash -= player.bet;
            while (player.Move())
            {
               Console.Out.WriteLine($"NextMove:{player.NextMove}");
               sb.AppendLine($"NextMove:{player.NextMove}");

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

               playerHand = $"Card:{player.hand.Cards[0].ValString};Card:{player.hand.Cards[1].ValString};pts:{player.hand.Points};";
               Console.Out.WriteLine(playerHand);
               sb.AppendLine(playerHand);
            }
         }
         
         var dealerHand = $"DealerCard:{dealer.hand.Cards[0].ValString};Card:{dealer.hand.Cards[1].ValString};pts:{dealer.hand.Points};";
         Console.Out.WriteLine(dealerHand);
         sb.AppendLine(dealerHand);

         //Dealers turn to make his moves
         while (dealer.Move())
         {
            Console.Out.WriteLine($"DealerMove:{dealer.NextMove}");
            sb.AppendLine($"DealerMove:{dealer.NextMove}");

            if (dealer.NextMove == 'h')
            {
               dealer.hand.Cards.Add(mainDeck.cards.Pop());
            }

            dealerHand = $"DealerPts:{dealer.hand.Points};";
            Console.Out.WriteLine(dealerHand);
            sb.AppendLine(dealerHand);
         }
         //Splitting the following into two
         //methods makes it loop over the players twice
         //but I think makes the code simpler...
         DeterminePlayerStatuses();
         PayPlayers();
         DisposeOfCards();
      }
   }

    private int GetNewBet(Player player)
    {
        //boringBet is 5 every time
        //else we increase bet after a win by a half
        var newBet = 5;

        if (boringBet)
        {
            return newBet;
        }

        //This should mean they won the last hand
        if(player.Status == 'w')
        {
            newBet = (int)(player.bet * 1.5);
        }

        return newBet;
    }

    private void DisposeOfCards()
    {
        foreach(var player in Players)
           player.hand.Cards.Clear();

        dealer.hand.Cards.Clear();
    }

    private void PayPlayers()
    {
        foreach (var player in Players)
        {
            Console.Out.WriteLine($"Player status: {player.Status};Cash: ${player.cash};PlayerPts: {player.hand.Points}");
            sb.AppendLine($"Player status: {player.Status};Cash: ${player.cash};PlayerPts: {player.hand.Points}");

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
            
            Console.Out.WriteLine($"PlayerCashAfter: ${player.cash}");
            sb.AppendLine($"PlayerCashAfter: ${player.cash}");
        }
    }

   private void DeterminePlayerStatuses()
   {
      var dealerPoints = dealer.hand.Points;
      var dealerBusted = dealerPoints > 21;
      
      Console.Out.WriteLine($"DealerPts: {dealerPoints}");
      sb.AppendLine($"DealerPts: {dealerPoints}");

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