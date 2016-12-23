using System.Collections.Generic;
namespace Jackal
{
   public class BlackJackSim
   {
      //Used to calculate when to shuffle cards
      public int AmtOfDecks {get;set;}
      public List<Player> Players {get;set;}
      public Dealer dealer {get;set;}
      public Deck mainDeck = null;

      public BlackJackSim(int amtOfDecks, int players, int startingAmt = 1000, int betAmt = 5, IBettingStrategy bettingStrategy = null)
      {
         AmtOfDecks = amtOfDecks;
         dealer = new Dealer();
         mainDeck = new Deck(amtOfDecks);
         Players = new List<Player>();

         for (int i=0; i<players;i++)
         {
            Players.Add(new Player($"Player#{i+1}")
            { 
               cash = startingAmt, 
               bet = betAmt, 
               BaseBet = betAmt, 
               BettingStrategy = bettingStrategy == null ? new BoringBetting() : bettingStrategy 
            });
         }
      }

      public void SimulateRounds(int rounds)
      {
         for (int i=0; i < rounds; i++)
         {
            CustomLogger.Log($"Round: {i+1}");
            
            PlaceBets();
            DealCards();

            //TODO: If dealer has a ten or Ace need to check for possible BlackJack
            CustomLogger.Log($"DealerUpCard:{dealer.hand.Cards[0].ValString};");

            //Let each player make their moves
            foreach (var player in Players)
            {
               CustomLogger.Log(player);
               var done = false;
               while (!done && player.Move())
               {
                  CustomLogger.Log($"NextMove:{player.NextMove}");
                  switch(player.NextMove)
                  {
                     case 'h'://hit
                        player.hand.Cards.Add(mainDeck.cards.Pop());
                        break;
                     case 's'://stand
                        break;
                     case 'd'://double
                        player.hand.Cards.Add(mainDeck.cards.Pop());
                        done = true;
                        break;
                     case 'y'://split
                        break;
                     default:
                        break;
                  }
                  CustomLogger.Log(player);
               }
            }
            
            CustomLogger.Log($"DealerCard:{dealer.hand.Cards[0].ValString};Card:{dealer.hand.Cards[1].ValString};pts:{dealer.hand.Points};");

            //Dealers turn to make his moves
            while (dealer.Move())
            {
               CustomLogger.Log($"DealerMove:{dealer.NextMove}");

               if (dealer.NextMove == 'h')
               {
                  dealer.hand.Cards.Add(mainDeck.cards.Pop());
               }

               CustomLogger.Log($"DealerPts:{dealer.hand.Points};");
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

         dealer.hand.Cards.Clear();
      }

      private void PayPlayers()
      {
         foreach (var player in Players)
         {
            CustomLogger.Log($"Player status: {player.Status};Cash: ${player.cash};PlayerPts: {player.hand.Points}");

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
            
            CustomLogger.Log($"PlayerCashAfter: ${player.cash}");
         }
      }

      private void DeterminePlayerStatuses()
      {
         var dealerPoints = dealer.hand.Points;
         var dealerBusted = dealerPoints > 21;
         
         CustomLogger.Log($"DealerPts: {dealerPoints}");

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

      public void PlaceBets()
      {
         foreach (var player in Players)
         {
            player.bet = player.GetNewBet();
            player.cash -= player.bet;
         }
      }
   }
}