using System;

namespace ConsoleApplication
{
   public class Program
   {
      public static void Main(string[] args)
      {
         try{
            var amtDecks = 2;
            var amtPlayers = 2;
            var rounds = 200;
            Console.Out.WriteLine($"Rounds: {rounds}; Decks: {amtDecks}; Players: {amtPlayers};");

            BlackJack blackJack = new BlackJack(amtDecks, amtPlayers);
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
