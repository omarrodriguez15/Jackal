using System;
using System.Text;

namespace Jackal
{
   public class CustomLogger 
   {
      private static StringBuilder _sb = new StringBuilder();
      private static bool _consoleOut = true;

      public static bool ConsoleOut 
      {
         set{ _consoleOut = value;}
         get{ return _consoleOut;}
      }

      public static void Log(string msg, bool consoleOut = true, bool tofile = false)
      {
         if(_consoleOut)
            Console.Out.WriteLine(msg);
         _sb.AppendLine(msg);
      }
      public static void Log(Player player)
      {
         var msg = $"{player.Name}:Card:{player.hand.Cards[0].ValString};Card:{player.hand.Cards[1].ValString};pts:{player.hand.Points};";
         Log(msg);
      }

      public static string GetLogString()
      {
         return _sb.ToString();
      }
      public static void ClearLog()
      {
         _sb.Clear();
      }
   }
}