using System;
using System.IO;

namespace Jackal
{
   public class Program
   {
      public static void Main(string[] args)
      {
         try{
            int amtDecks = 2, amtPlayers = 2, rounds = 200;

            Console.Out.WriteLine($"Rounds: {rounds}; Decks: {amtDecks}; Players: {amtPlayers};");

            BlackJackSim blackJack = new BlackJackSim(amtDecks, amtPlayers);
            blackJack.SimulateRounds(rounds);

            foreach(var player in blackJack.Players)
            {
               Console.Out.WriteLine($"Player: Cash: ${player.cash}");
            }
         }
         catch(Exception ex)
         {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
         }
      }
   }
}
