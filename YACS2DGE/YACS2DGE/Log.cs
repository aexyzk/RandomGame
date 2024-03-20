using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
// add timestape
namespace YACS2DGE.YACS2DGE
{
    public class Log
    {
        public static void Info(string msg)
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("HH:mm:ss");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{formattedTime}] [Info] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string msg)
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("HH:mm:ss");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{formattedTime}] [Error] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warn(string msg)
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("HH:mm:ss");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{formattedTime}] [Warning] {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}