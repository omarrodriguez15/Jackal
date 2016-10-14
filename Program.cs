using System;
using System.IO;

namespace ConsoleApplication
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var outputFile = "results";
         var logFolder = "output";
         outputFile = GetLogFileName(logFolder, outputFile);
         
         using (StreamWriter sw = File.AppendText(outputFile))
         try{
            var amtDecks = 2;
            var amtPlayers = 2;
            var rounds = 200;

            Console.SetOut(sw);
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

      private static string GetLogFileName(string logFolder, string fileName)
      {
         if (!Directory.Exists(logFolder))
         {
            Directory.CreateDirectory(logFolder);
            Console.Out.WriteLine($"Created directory {logFolder}");
         }

         var files = Directory.GetFiles(logFolder);

         return Path.Combine(logFolder, $"{fileName}{files.Length}.log");
      }
    }
}
