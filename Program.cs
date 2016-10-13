using System;

namespace ConsoleApplication
{
   public class Program
   {
      public static void Main(string[] args)
      {
         try{
            Console.WriteLine("");
            var amtDecks = 2;
            var amtPlayers = 2;
            BlackJack blackJack = new BlackJack(amtDecks, amtPlayers);
            blackJack.SimulateRounds(1);

            foreach(var player in blackJack.Players)
            {
               Console.WriteLine($"Player: Cash: ${player.cash}");
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
